// Get element by id
function $(id) {
    return document.getElementById(id);
}

// Show a message in #pageMsg
function setMsg(text, isError = false) {
    const msgEl = $("pageMsg");
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

    if (!res.ok) throw new Error(`HTTP ${res.status}`);
    // Some APIs return nothing on PUT, so we try safely:
    const text = await res.text();
    return text ? JSON.parse(text) : null;
}