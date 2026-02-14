// Get element by id
function $(id) {
    return document.getElementById(id);
}

// Show a message in #pageMsg
function setMsg(elementId, text, isError = false) {
    const msgEl = $(elementId);
    if (!msgEl) return;

    msgEl.textContent = text || "";
    msgEl.style.color = isError ? "#ff8b8b" : "rgba(232,238,252,.65)";
}

// Prevent HTML injection
function escapeHtml(str) {
    return String(str ?? "")
        .replaceAll("&", "&amp;")
        .replaceAll("<", "&lt;")
        .replaceAll(">", "&gt;")
        .replaceAll('"', "&quot;")
        .replaceAll("'", "&#039;");
}

// Read query string: ?id=5
function getQueryParam(name) {
    const params = new URLSearchParams(window.location.search);
    return params.get(name);
}

// Small wrapper: GET JSON
async function apiGetJson(url) {
    const res = await fetch(url, {
        headers: { Accept: "application/json" }
    });
    if (!res.ok) throw new Error(`HTTP ${res.status}`);
    return await res.json();
}

// Small wrapper: POST/PUT JSON
async function apiSendJson(url, method, bodyObj) {
    const res = await fetch(url, {
        method,
        headers: { "Content-Type": "application/json", Accept: "application/json" },
        body: JSON.stringify(bodyObj),
    });

    // If backend returns 400/500, read details to help debugging
    if (!res.ok) {
        const errText = await res.text().catch(() => "");
        throw new Error(`HTTP ${res.status} - ${errText}`);
    }

    // 204 No Content
    if (res.status === 204) return null;

    // If content-type is JSON, parse JSON safely
    const contentType = res.headers.get("content-type") || "";
    if (contentType.includes("application/json")) {
        return await res.json();
    }

    // Otherwise return text (could be empty or plain text)
    return await res.text();
}
