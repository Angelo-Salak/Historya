using UnityEngine;
using System.Collections;
using Paroxe.PdfRenderer.WebGL;
using UnityEngine.UI;

namespace Paroxe.PdfRenderer.Examples
{
    public class WebGL_API_Usage : MonoBehaviour
    {
        public RawImage m_RawImage;

        private IEnumerator Start()
        {
            Debug.Log("WebGLTest: ");

            PDFJS_Promise<PDFDocument> documentPromise = PDFDocument.LoadDocumentFromBytesAsync(PDFBytesSupplierExample.PDFSampleByteArray);

            while (!documentPromise.HasFinished)
                yield return null;

            if (!documentPromise.HasSucceeded)
            {
                Debug.Log("Fail: documentPromise");

                yield break;
            }

            Debug.Log("Success: documentPromise");

            PDFDocument document = documentPromise.Result;

            PDFJS_Promise<PDFPage> pagePromise = document.GetPageAsync(0);

            while (!pagePromise.HasFinished)
                yield return null;

            if (!pagePromise.HasSucceeded)
            {
                Debug.Log("Fail: pagePromise");

                yield break;
            }

            Debug.Log("Success: pagePromise");

            PDFPage page = pagePromise.Result;

            PDFJS_Promise<Texture2D> renderPromise = PDFRenderer.RenderPageToTextureAsync(page, (int)page.GetPageSize().x, (int)page.GetPageSize().y);

            while (!renderPromise.HasFinished)
                yield return null;

            if (!renderPromise.HasSucceeded)
            {
                Debug.Log("Fail: pagePromise");
                yield break;
            }

            Texture2D renderedPageTexture = renderPromise.Result;

            ((RectTransform)m_RawImage.transform).sizeDelta = new Vector2(renderedPageTexture.width, renderedPageTexture.height);
            m_RawImage.texture = renderedPageTexture;
        }
    }
}
