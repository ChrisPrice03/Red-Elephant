using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine;

public class ScreenshotTaker : MonoBehaviour
{
    private string screenshotsDirectory = "red-elephant-screenshots";
    private string screenshotsFullPath;

    void Start()
{
    // Set the full path to your specific folder.
    screenshotsFullPath = @"C:\Users\Owner\Videos\cs307\Red-Elephant-main\red-elephant-screenshots";

    // Create the directory if it doesn't already exist.
    if (!Directory.Exists(screenshotsFullPath))
    {
        Directory.CreateDirectory(screenshotsFullPath);
    }
}

    void Update()
    {
        // Listen for the F12 key press.
        if (Input.GetKeyDown(KeyCode.F12))
        {
            // Generate a unique filename for each screenshot based on the current date and time.
            string fileName = "Screenshot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
            string filePath = Path.Combine(screenshotsFullPath, fileName);

            // Capture the screenshot and save it to the specified path.
            ScreenCapture.CaptureScreenshot(filePath);

            // Optionally, log the path of the saved screenshot for confirmation.
            Debug.Log("Screenshot saved to: " + filePath);
        }
    }
}