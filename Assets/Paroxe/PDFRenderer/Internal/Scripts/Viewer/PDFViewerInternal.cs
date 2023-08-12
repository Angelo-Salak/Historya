using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#pragma warning disable 649

namespace Paroxe.PdfRenderer.Internal.Viewer
{
    public class PDFViewerInternal : UIBehaviour
    {
	    [SerializeField]
	    private PDFViewer m_PDFViewer;
        [SerializeField]
        private RectTransform m_DownloadDialog;
        [SerializeField]
        private Text m_DownloadSourceLabel;
        [SerializeField]
        private RectTransform m_HorizontalScrollBar;
        [SerializeField]
        private Image m_InvalidPasswordImage;
        [SerializeField]
        private PDFViewerLeftPanel m_LeftPanel;
        [SerializeField]
        private CanvasGroup m_Overlay;
        [SerializeField]
        private RectTransform m_PageContainer;
        [SerializeField]
        private Text m_PageCountLabel;
        [SerializeField]
        private Button m_PageDownButton;
        [SerializeField]
        private InputField m_PageInputField;
        [SerializeField]
        private RawImage m_PageSample;
        [SerializeField]
        private Button m_PageUpButton;
        [SerializeField]
        private Text m_PageZoomLabel;
        [SerializeField]
        private RectTransform m_PasswordDialog;
        [SerializeField]
        private InputField m_PasswordInputField;
        [SerializeField]
        private Text m_ProgressLabel;
        [SerializeField]
        private RectTransform m_ProgressRect;
        [SerializeField]
        private RectTransform m_ScrollCorner;
        [SerializeField]
        private ScrollRect m_ScrollRect;
        [SerializeField]
        private RectTransform m_TopPanel;
        [SerializeField]
        private RectTransform m_VerticalScrollBar;
        [SerializeField]
        private RectTransform m_Viewport;
        [SerializeField]
        private RectTransform m_SearchPanel;

        public RectTransform DownloadDialog { get { return m_DownloadDialog; } }
        public Text DownloadSourceLabel { get { return m_DownloadSourceLabel; } }
        public RectTransform HorizontalScrollBar { get { return m_HorizontalScrollBar; } }
        public Image InvalidPasswordImage { get { return m_InvalidPasswordImage; } }
        public PDFViewerLeftPanel LeftPanel { get { return m_LeftPanel; } }
        public CanvasGroup Overlay { get { return m_Overlay; } }
        public RectTransform PageContainer { get { return m_PageContainer; } }
        public Text PageCountLabel { get { return m_PageCountLabel; } }
        public RawImage PageSample { get { return m_PageSample; } }
        public Text PageZoomLabel { get { return m_PageZoomLabel; } }
        public RectTransform PasswordDialog { get { return m_PasswordDialog; } }
        public Text ProgressLabel { get { return m_ProgressLabel; } }
        public RectTransform ProgressRect { get { return m_ProgressRect; } }
        public RectTransform ScrollCorner { get { return m_ScrollCorner; } }
        public ScrollRect ScrollRect { get { return m_ScrollRect; } }
        public RectTransform TopPanel { get { return m_TopPanel; } }
        public RectTransform VerticalScrollBar { get { return m_VerticalScrollBar; } }
        public RectTransform Viewport { get { return m_Viewport; } }
        public RectTransform SearchPanel { get { return m_SearchPanel; } }
        public InputField PasswordInputField { get { return m_PasswordInputField; } }
        public InputField PageInputField { get { return m_PageInputField; } }

        public void OnDownloadCancelButtonClicked()
        {
	        m_PDFViewer.CancelDownload();
        }

        public void OnNextPageButtonClicked()
        {
	        m_PDFViewer.GoToNextPage();
        }

        public void OnPageIndexEditEnd()
        {
	        m_PDFViewer.OnPageEditEnd();
        }

        public void OnPasswordDialogCancelButtonClicked()
        {
	        m_PDFViewer.OnPasswordDialogCancelButtonClicked();
        }

        public void OnPasswordDialogOkButtonClicked()
        {
	        m_PDFViewer.OnPasswordDialogOkButtonClicked();
        }

        public void OnPreviousPageButtonClicked()
        {
	        m_PDFViewer.GoToPreviousPage();
        }

        public void OnZoomInButtonClicked()
        {
	        m_PDFViewer.ZoomIn();
        }

        public void OnZoomOutButtonClicked()
        {
	        m_PDFViewer.ZoomOut();
        }
    }
}