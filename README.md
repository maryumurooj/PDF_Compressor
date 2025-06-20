# 📄 PDF Compressor API (ASP.NET Core)

This is a simple, lightweight API built using **ASP.NET Core (.NET 9)** that compresses PDF files. 

- 🗂 Reads PDFs from `wwwroot/input/`
- 🗜 Compresses each PDF
- 📤 Stores the compressed PDFs into `wwwroot/output/`
- 🎯 Supports both structure-level compression (via iTextSharp) and aggressive compression (via Ghostscript, optional)

---

## 🚀 Features

- Simple POST API endpoint to trigger batch compression
- Auto-detects all PDFs in the input folder
- Lightweight setup – no database required
- Static file support for hosting input/output
- Extensible: plug in Ghostscript for better compression

---

## 📂 Project Structure

```

PdfCompressorAPI/
├── wwwroot/
│   ├── input/          # Place original PDFs here
│   └── output/         # Compressed PDFs will appear here
├── Controllers/
│   └── CompressController.cs
├── Services/
│   ├── PdfCompressor.cs         # Uses iTextSharp for basic compression
│   └── GhostscriptCompressor.cs # (Optional) Aggressive compression
├── Program.cs
└── PdfCompressorAPI.csproj

```

---

## 🛠 Installation & Usage

### Step 1: Clone and Install

```bash
git clone https://github.com/your-username/pdf-compressor-api.git
cd pdf-compressor-api
dotnet restore
```

### Step 2: Add Your PDFs

Place all PDFs to be compressed in:

```
wwwroot/input/
```

### Step 3: Run the API

```bash
dotnet run
```

### Step 4: Trigger Compression

Use any HTTP client (Postman, curl, etc.):

```bash
curl -X POST http://localhost:5000/api/compress/compress-all
```

Compressed files will appear in:

```
wwwroot/output/
```

