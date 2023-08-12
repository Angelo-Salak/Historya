using Paroxe.PdfRenderer.WebGL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Paroxe.PdfRenderer.Internal;
using System.IO;

namespace Paroxe.PdfRenderer
{
    /// <summary>
    /// Represents a PDF document. This class is the entry point of all functionalities.
    /// </summary>
    public class PDFDocument : IPDFDocumentInternal, IDisposable
    {
        private bool m_Disposed;
        private bool m_Disposing;
        private IntPtr m_NativePointer;
        private GCHandle m_BufferHandle;
        private byte[] m_DocumentBuffer;
        private bool m_ValidDocument;
        private PDFRenderer m_Renderer;
        private HashSet<PDFPage> m_LoadedPages;

        public static PDFJS_Promise<PDFDocument> LoadDocumentFromUrlAsync(string url)
        {
            PDFJS_Promise<PDFDocument> documentPromise = new PDFJS_Promise<PDFDocument>();

#if !UNITY_WEBGL || UNITY_EDITOR

            PDFJS_Library.Instance.PreparePromiseCoroutine(LoadDocumentFromWWWCoroutine, documentPromise, url).Start();
#else
            LoadDocumentParameters parameters = new LoadDocumentParameters();
            parameters.url = url;

            PDFJS_Library.Instance.PreparePromiseCoroutine(LoadDocumentCoroutine, documentPromise, parameters).Start();
#endif

            return documentPromise;
        }

#if !UNITY_WEBGL || UNITY_EDITOR
        private static IEnumerator LoadDocumentFromWWWCoroutine(PDFJS_PromiseCoroutine promiseCoroutine, IPDFJS_Promise promise, object urlString)
        {
            PDFJS_Promise<PDFDocument> documentPromise = promise as PDFJS_Promise<PDFDocument>;

            PDFLibrary.Instance.EnsureInitialized();
            while (!PDFLibrary.Instance.IsInitialized)
                yield return null;

            string url = urlString as string;

            PDFWebRequest www = new PDFWebRequest(url);
            www.SendWebRequest();

            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                documentPromise.HasFinished = true;
                documentPromise.HasSucceeded = true;
                documentPromise.HasReceivedJSResponse = true;
                documentPromise.Result = new PDFDocument(www.bytes);

                promiseCoroutine.ExecuteThenAction(true, documentPromise.Result);
            }
            else
            {
                documentPromise.HasFinished = true;
                documentPromise.HasSucceeded = false;

                promiseCoroutine.ExecuteThenAction(false, null);
            }

            www.Dispose();
            www = null;
        }
#endif

        public static PDFJS_Promise<PDFDocument> LoadDocumentFromBytesAsync(byte[] bytes)
        {
            PDFJS_Promise<PDFDocument> documentPromise = new PDFJS_Promise<PDFDocument>();

#if !UNITY_WEBGL || UNITY_EDITOR
            documentPromise.HasFinished = true;
            documentPromise.HasSucceeded = true;
            documentPromise.HasReceivedJSResponse = true;
            documentPromise.Result = new PDFDocument(bytes);
#else
            LoadDocumentParameters parameters = new LoadDocumentParameters();
            parameters.bytes = bytes;

            PDFJS_Library.Instance.PreparePromiseCoroutine(LoadDocumentCoroutine, documentPromise, parameters).Start();
#endif

            return documentPromise;
        }

        public PDFDocument(IntPtr nativePointer)
        {
            PDFLibrary.AddRef("PDFDocument");

            m_NativePointer = nativePointer;
            m_ValidDocument = true;
        }

#if !UNITY_WEBGL || UNITY_EDITOR
        /// <summary>
        /// Open PDF Document with the specified byte array.
        /// </summary>
        /// <param name="buffer"></param>
        public PDFDocument(byte[] buffer)
            : this(buffer, "")
        { }

        /// <summary>
        /// Open PDF Document with the specified byte array.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="password">Can be null or empty</param>
        public PDFDocument(byte[] buffer, string password)
        {
            PDFLibrary.AddRef("PDFDocument");

            CommonInit(buffer, password);
        }

        /// <summary>
        /// Open PDF Document located at the specified path
        /// </summary>
        /// <param name="filePath"></param>
        public PDFDocument(string filePath)
            : this(filePath, "")
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="password">Can be null or empty</param>
        public PDFDocument(string filePath, string password)
        {
            PDFLibrary.AddRef("PDFDocument");

            CommonInit(File.ReadAllBytes(filePath), password);
        }

#endif

        ~PDFDocument()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool _)
        {
	        if (!m_Disposed)
	        {
		        m_Disposing = true;

                lock (PDFLibrary.nativeLock)
                {
                    if (m_NativePointer != IntPtr.Zero)
                    {
	                    if (m_LoadedPages != null)
	                    {
		                    foreach (PDFPage page in m_LoadedPages)
			                    page.Dispose();

		                    m_LoadedPages = null;
                        }

#if UNITY_WEBGL && !UNITY_EDITOR
                        NativeMethods.PDFJS_CloseDocument(m_NativePointer.ToInt32());
#else
                        NativeMethods.FPDF_CloseDocument(m_NativePointer);
#endif
                        if (m_DocumentBuffer != null)
                            m_BufferHandle.Free();

                        m_NativePointer = IntPtr.Zero;
                    }
                }

                PDFLibrary.RemoveRef("PDFDocument");

                m_Disposing = false;
                m_Disposed = true;
            }
        }

        void IPDFDocumentInternal.OnPageClose(PDFPage page)
        {
	        if (m_LoadedPages != null && !m_Disposing)
		        m_LoadedPages.Remove(page);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Return a convenience PDFRenderer instance. 
        /// </summary>
        public PDFRenderer Renderer
        {
            get
            {
                if (m_Renderer == null)
                    m_Renderer = new PDFRenderer();
                return m_Renderer;
            }
        }

        /// <summary>
        /// The byte array of the document.
        /// </summary>
        public byte[] DocumentBuffer
        {
            get { return m_DocumentBuffer; }
        }

        /// <summary>
        /// Return if the document is valid. The document can be invalid if the password is invalid or if the
        /// document itseft is corrupted. See PDFLibrary.GetLastError.
        /// </summary>
        public bool IsValid
        {
            get { return m_ValidDocument; }
        }

        public IntPtr NativePointer
        {
            get { return m_NativePointer; }
        }

        public int GetPageCount()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return NativeMethods.PDFJS_GetPageCount(m_NativePointer.ToInt32());
#else
            return NativeMethods.FPDF_GetPageCount(m_NativePointer);
#endif
        }

#if !UNITY_WEBGL || UNITY_EDITOR
        public Vector2 GetPageSize(int pageIndex)
        {
            double width;
            double height;

            NativeMethods.FPDF_GetPageSizeByIndex(m_NativePointer, pageIndex, out width, out height);

            return new Vector2((float)width, (float)height);
        }
#endif

#if !UNITY_WEBGL || UNITY_EDITOR
        public int GetPageWidth(int pageIndex)
        {
            double width;
            double height;

            NativeMethods.FPDF_GetPageSizeByIndex(m_NativePointer, pageIndex, out width, out height);

            return (int)width;
        }
#endif

#if !UNITY_WEBGL || UNITY_EDITOR
        public int GetPageHeight(int pageIndex)
        {
            double width;
            double height;

            NativeMethods.FPDF_GetPageSizeByIndex(m_NativePointer, pageIndex, out width, out height);

            return (int)height;
        }
#endif

#if !UNITY_WEBGL
        /// <summary>
        /// Return the root bookmark of the document.
        /// </summary>
        /// <returns></returns>
        public PDFBookmark GetRootBookmark()
        {
            return GetRootBookmark(null);
        }
#endif

#if !UNITY_WEBGL
        /// <summary>
        /// Return the root bookmark of the document.
        /// </summary>
        /// <param name="device">Pass the device that will receive bookmarks action</param>
        /// <returns></returns>
        public PDFBookmark GetRootBookmark(IPDFDevice device)
        {
            return new PDFBookmark(this, null, IntPtr.Zero, device);
        }
#endif

#if !UNITY_WEBGL || UNITY_EDITOR
        public PDFPage GetPage(int index)
        {
            PDFPage page = new PDFPage(this, index);

            if (m_LoadedPages == null)
	            m_LoadedPages = new HashSet<PDFPage>();

            m_LoadedPages.Add(page);

            return page;
        }
#endif

        public PDFJS_Promise<PDFPage> GetPageAsync(int index)
        {
            return PDFPage.LoadPageAsync(this, index);
        }

        private void CommonInit(byte[] buffer, string password)
        {
            m_DocumentBuffer = buffer;

            if (m_DocumentBuffer != null)
            {
#if !UNITY_WEBGL || UNITY_EDITOR

                m_BufferHandle = GCHandle.Alloc(m_DocumentBuffer, GCHandleType.Pinned);

                m_NativePointer = NativeMethods.FPDF_LoadMemDocument(m_BufferHandle.AddrOfPinnedObject(), m_DocumentBuffer.Length, password);
#endif

                m_ValidDocument = (m_NativePointer != IntPtr.Zero);
            }
            else
            {
                m_ValidDocument = false;
            }
        }

#if UNITY_WEBGL && !UNITY_EDITOR
        class LoadDocumentParameters
        {
            public string url;
            public byte[] bytes;
        }

        private static IEnumerator LoadDocumentCoroutine(PDFJS_PromiseCoroutine promiseCoroutine, IPDFJS_Promise promise, object pars)
        {
            PDFJS_Promise<PDFDocument> documentPromise = promise as PDFJS_Promise<PDFDocument>;

            PDFLibrary.Instance.EnsureInitialized();
            while (!PDFLibrary.Instance.IsInitialized)
                yield return null;

            LoadDocumentParameters parameters = pars as LoadDocumentParameters;

            if (!string.IsNullOrEmpty(parameters.url))
                NativeMethods.PDFJS_LoadDocumentFromURL(promise.PromiseHandle, parameters.url);
            else
                NativeMethods.PDFJS_LoadDocumentFromBytes(promise.PromiseHandle, Convert.ToBase64String(parameters.bytes));

            while (!promiseCoroutine.Promise.HasReceivedJSResponse)
                yield return null;

            if (documentPromise.HasSucceeded)
            {
                int documentHandle = int.Parse(promiseCoroutine.Promise.JSObjectHandle);
                PDFDocument document = new PDFDocument(new IntPtr(documentHandle));

                documentPromise.Result = document;
                documentPromise.HasFinished = true;

                promiseCoroutine.ExecuteThenAction(true, documentHandle);
            }
            else
            {
                documentPromise.Result = null;
                documentPromise.HasFinished = true;

                promiseCoroutine.ExecuteThenAction(false, null);
            }
        }
#endif
    }

    public interface IPDFDocumentInternal
    {
	    void OnPageClose(PDFPage page);
    }
}