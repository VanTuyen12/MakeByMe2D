using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerFloatingMessageBox : MonoBehaviour
{
   [Header("References")]
    [SerializeField] private Transform playerTransform;       // Transform của player
    [SerializeField] private GameObject messageBoxObject;     // GameObject chứa hộp thông báo
    [SerializeField] private TextMeshProUGUI messageText;     // Component text để hiển thị thông báo
    
    [Header("Position Settings")]
    [SerializeField] private Vector3 offset = new Vector3(0, 1.5f, 0);  // Vị trí hiển thị so với player
    
    [Header("Timing")]
    [SerializeField] private float showInterval = 10f;        // Thời gian giữa mỗi lần hiển thị (giây)
    [SerializeField] private float displayDuration = 3f;      // Thời gian hiển thị (giây)
    [SerializeField] private float fadeTime = 0.5f;           // Thời gian fade in/out (giây)
    
    [Header("Messages")]
    [SerializeField] private string[] messages;               // Danh sách thông báo để hiển thị
    [SerializeField] private bool randomMessage = true;       // Chọn ngẫu nhiên hay theo thứ tự
    
    private CanvasGroup canvasGroup;                         // Để làm hiệu ứng fade
    private int currentMessageIndex = 0;                     // Chỉ số thông báo hiện tại
    private bool isShowingMessage = false;                   // Trạng thái hiển thị
    
    private void Awake()
    {
        // Tự động tìm player nếu chưa được set
        if (playerTransform == null)
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        
        // Lấy component CanvasGroup (nếu không có thì thêm mới)
        canvasGroup = messageBoxObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = messageBoxObject.AddComponent<CanvasGroup>();
            
        // Ban đầu ẩn box
        messageBoxObject.SetActive(false);
        canvasGroup.alpha = 0;
    }
    
    private void Start()
    {
        
        // Bắt đầu chu kỳ hiển thị
        StartCoroutine(ShowMessagePeriodically());
    }
    
    private void LateUpdate()
    {
        // Cập nhật vị trí message box theo player
        if (playerTransform != null)
        {
            messageBoxObject.transform.position = playerTransform.position + offset;
        }
    }
    
    // Coroutine điều khiển chu kỳ hiển thị
    private IEnumerator ShowMessagePeriodically()
    {
        while (true)
        {
            // Đợi khoảng thời gian giữa các lần hiển thị
            yield return new WaitForSeconds(showInterval);
            
            // Hiển thị hộp thông báo
            yield return StartCoroutine(ShowMessage());
        }
    }
    
    // Hiển thị hộp thông báo
    private IEnumerator ShowMessage()
    {
        if (isShowingMessage)
            yield break;
            
        isShowingMessage = true;
        
        // Chọn thông báo để hiển thị
        string messageToShow = GetNextMessage();
        if (string.IsNullOrEmpty(messageToShow))
        {
            isShowingMessage = false;
            yield break;
        }
            
        // Cập nhật nội dung
        messageText.text = messageToShow;
        
        // Hiển thị box
        messageBoxObject.SetActive(true);
        
        // Hiệu ứng fade in
        yield return FadeCanvas(0f, 1f, fadeTime);
        
        // Hiển thị trong một khoảng thời gian
        yield return new WaitForSeconds(displayDuration);
        
        // Hiệu ứng fade out
        yield return FadeCanvas(1f, 0f, fadeTime);
        
        // Ẩn box
        messageBoxObject.SetActive(false);
        
        isShowingMessage = false;
    }
    
    // Hiệu ứng fade cho canvas
    private IEnumerator FadeCanvas(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        canvasGroup.alpha = endAlpha;
    }
    
    // Lấy thông báo tiếp theo để hiển thị
    private string GetNextMessage()
    {
        if (messages == null || messages.Length == 0)
            return "No message available";
            
        string message;
        
        if (randomMessage)
        {
            // Chọn ngẫu nhiên một thông báo
            int index = Random.Range(0, messages.Length);
            message = messages[index];
        }
        else
        {
            // Chọn theo thứ tự
            message = messages[currentMessageIndex];
            currentMessageIndex = (currentMessageIndex + 1) % messages.Length;
        }
        
        return message;
    }
    
    // Phương thức public để thiết lập danh sách thông báo từ bên ngoài
    public void SetMessages(string[] newMessages)
    {
        messages = newMessages;
        currentMessageIndex = 0;
    }
    
    // Hiển thị message cụ thể ngay lập tức
    public void ShowMessageNow(string message)
    {
        StopAllCoroutines();
        StartCoroutine(ShowSpecificMessage(message));
    }
    
    // Hiển thị một thông báo cụ thể
    private IEnumerator ShowSpecificMessage(string message)
    {
        isShowingMessage = true;
        
        // Cập nhật nội dung
        messageText.text = message;
        
        // Hiển thị box
        messageBoxObject.SetActive(true);
        
        // Hiệu ứng fade in
        yield return FadeCanvas(0f, 1f, fadeTime);
        
        // Hiển thị trong một khoảng thời gian
        yield return new WaitForSeconds(displayDuration);
        
        // Hiệu ứng fade out
        yield return FadeCanvas(1f, 0f, fadeTime);
        
        // Ẩn box
        messageBoxObject.SetActive(false);
        
        isShowingMessage = false;
        
        // Khởi động lại chu kỳ hiển thị
        StartCoroutine(ShowMessagePeriodically());
    }
}
