using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Paroxe.PdfRenderer.Internal
{
	public static class NativeMethods
	{
#if !UNITY_WEBGL || UNITY_EDITOR
        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern IntPtr FPDFAction_GetDest(IntPtr document, IntPtr action);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern uint FPDFAction_GetFilePath(IntPtr action, [In, Out] byte[] buffer, uint buflen);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern uint FPDFAction_GetType(IntPtr action);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern uint FPDFAction_GetURIPath(IntPtr document, IntPtr action, [In, Out] byte[] buffer, uint buflen);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern uint FPDFDest_GetDestPageIndex(IntPtr document, IntPtr dest);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern IntPtr FPDFLink_GetAction(IntPtr link);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern IntPtr FPDFLink_GetDest(IntPtr document, IntPtr link);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern IntPtr FPDFBookmark_GetAction(IntPtr bookmark);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern IntPtr FPDFBookmark_GetDest(IntPtr document, IntPtr bookmark);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern IntPtr FPDFBookmark_GetFirstChild(IntPtr document, IntPtr bookmark);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern IntPtr FPDFBookmark_GetNextSibling(IntPtr document, IntPtr bookmark);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern uint FPDFBookmark_GetTitle(IntPtr bookmark, [In, Out] byte[] buffer, uint buflen);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void FPDF_CloseDocument(IntPtr document);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern uint FPDF_GetDocPermissions(IntPtr document);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern int FPDF_GetPageCount(IntPtr document);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY, CharSet = CharSet.Ansi)]
        public static extern IntPtr FPDF_LoadMemDocument(IntPtr data_buf, int size, string password);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern int FPDF_GetPageSizeByIndex(IntPtr document, int page_index, out double width, out double height);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern uint FPDF_GetLastError();

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void FPDF_DestroyLibrary();

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void FPDF_InitLibrary();

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern IntPtr FPDF_LoadPage(IntPtr document, int page_index);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void FPDF_ClosePage(IntPtr page);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern IntPtr FPDFLink_GetLinkAtPoint(IntPtr page, double x, double y);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void FPDF_DeviceToPage(IntPtr page, int start_x, int start_y, int size_x, int size_y, int rotate, int device_x, int device_y, out double page_x, out double page_y);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void FPDF_PageToDevice(IntPtr page, int start_x, int start_y, int size_x, int size_y, int rotate, double page_x, double page_y, out int device_x, out int device_y);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void FPDF_RenderPageBitmap(IntPtr bitmap, IntPtr page, int start_x, int start_y, int size_x, int size_y, int rotate, int flags);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void FPDF_RenderPageBitmapWithMatrix(IntPtr bitmap, IntPtr page, ref PDFRenderer.PDFMatrix matrix, ref PDFRenderer.PDFRect clipping, int flags);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern IntPtr FPDFText_FindStart(IntPtr text_page, IntPtr buffer, uint flags, int start_index);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void FPDFText_FindClose(IntPtr handle);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern bool FPDFText_FindNext(IntPtr handle);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern bool FPDFText_FindPrev(IntPtr handle);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern int FPDFText_GetSchCount(IntPtr handle);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern int FPDFText_GetSchResultIndex(IntPtr handle);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern IntPtr FPDFText_LoadPage(IntPtr page);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void FPDFText_ClosePage(IntPtr text_page);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern int FPDFText_CountChars(IntPtr text_page);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern int FPDFText_CountRects(IntPtr text_page, int start_index, int count);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern int FPDFText_GetBoundedText(IntPtr text_page, double left, double top, double right, double bottom, [In, Out] byte[] buffer, int buflen);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void FPDFText_GetCharBox(IntPtr text_page, int index, out double left, out double right, out double bottom, out double top);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern int FPDFText_GetCharIndexAtPos(IntPtr text_page, double x, double y, double xTolerance, double yTolerance);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern double FPDFText_GetFontSize(IntPtr text_page, int index);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void FPDFText_GetRect(IntPtr text_page, int rect_index, out double left, out double top, out double right, out double bottom);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern int FPDFText_GetText(IntPtr text_page, int start_index, int count, [In, Out] byte[] buffer);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern uint FPDFText_GetUnicode(IntPtr text_page, int index);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern IntPtr FPDFBitmap_Create(int width, int height, bool alpha);

        //[MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        //private static extern IntPtr FPDFBitmap_CreateEx(int width, int height, int format, IntPtr firstScan, int stride);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void FPDFBitmap_Destroy(IntPtr bitmap);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void FPDFBitmap_FillRect(IntPtr bitmap, int left, int top, int width, int height, int color);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern IntPtr FPDFBitmap_GetBuffer(IntPtr bitmap);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern int FPDFBitmap_GetStride(IntPtr bitmap);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern PDFBitmap.BitmapFormat FPDFBitmap_GetFormat(IntPtr bitmap);
#else
        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void PDFJS_LoadDocumentFromURL(string promiseHandle, string documentUrl);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void PDFJS_LoadDocumentFromBytes(string promiseHandle, string base64);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void PDFJS_CloseDocument(int document);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern int PDFJS_GetPageCount(int documentHandle);

		[MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void PDFJS_InitLibrary();

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void PDFJS_LoadPage(string promiseHandle, int documentHandle, int pageIndex);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void PDFJS_ClosePage(int pageHandle);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern int PDFJS_GetPageWidth(int pageHandle, float scale);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern int PDFJS_GetPageHeight(int pageHandle, float scale);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void PDFJS_RenderPageIntoCanvas(string promiseHandle, int pageHandle, float scale, float width, float height);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void PDFJS_RenderCanvasIntoTexture(int canvasHandle, int textureHandle);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void PDFJS_TryTerminateRenderWorker(string promiseHandle);

        [MethodImpl(MethodImplOptions.Synchronized), DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        public static extern void PDFJS_DestroyCanvas(int canvasHandle);
#endif
    }
}
