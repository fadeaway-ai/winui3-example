# WinUI 3 Browser Example

A WinUI 3 example project attempting to create a browser-like UI with WebView2.

## Status: Not Recommended

**We are not bullish on this approach.** The WebView2 control in WinUI 3 (especially with .NET 10) has issues rendering - the control appears in the visual tree but doesn't actually display web content. The initialization events don't fire as expected.

This repo is saved for reference purposes only. We recommend exploring alternative approaches for browser embedding in Windows apps.

## What's Here

- Basic WinUI 3 app structure
- Navigation bar with back/forward/refresh buttons and address bar
- WebView2 control (not rendering properly)
- File-based logging for debugging

## Known Issues

- WebView2 doesn't render any content
- `NavigationCompleted` and `NavigationStarting` events never fire
- `EnsureCoreWebView2Async()` hangs indefinitely
- Setting `Source` property completes without errors but nothing displays

## Requirements

- .NET 10
- Windows App SDK
- WebView2 Runtime (installed)

## Build

```bash
dotnet build
dotnet run
```
