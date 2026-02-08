// brands/brands.js

// 4) Load brands from the API and render them into the table
async function loadBrands(searchText = "") {
    // A) Find the table body where rows should be inserted
    const tbody = $("brandsRows");
    if (!tbody) return; // safety: page might not have this table

    // B) Show user feedback while loading
    setMsg("Loading brands...");

    // C) Build the URL (with optional search query)
    // If searchText is empty, call: /api/Brands
    // If not empty, call: /api/Brands?search=....
    const url = searchText && searchText.trim().length > 0
        ? `${BRANDS_API}?search=${encodeURIComponent(searchText.trim())}`
        : BRANDS_API;

    // D) Call the API (fetch sends an HTTP request)
    const res = await fetch(url, {
        headers: { Accept: "application/json" }
    });

    // E) If the response is not 200-299, show error and stop
    if (!res.ok) {
        setMsg("Failed to load brands from API.", true);
        return;
    }

    // F) Convert JSON response body into a JS object/array
    const brands = await res.json(); // expected: array of BrandDto

    // G) Clear old rows (important when searching / reloading)
    tbody.innerHTML = "";

    // H) Handle empty list
    if (!brands || brands.length === 0) {
        setMsg("No brands found.");
        return;
    }

    // I) Show success message
    setMsg(`Loaded ${brands.length} brand(s).`);

    // J) Render each brand into a <tr>
    brands.forEach((b, index) => {
        const id = b.id ?? b.ID ?? ""; // supports both naming styles
        const name = b.name ?? b.Name ?? "-";
        const desc = b.brandDescription ?? b.BrandDescription ?? b.description ?? b.Description ?? "";

        const tr = document.createElement("tr");
        tr.innerHTML = `
      <td>${index + 1}</td>
      <td><a class="brand-link" href="view.html?id=${id}">${escapeHtml(name)}</a></td>
      <td>${escapeHtml(desc)}</td>
      <td class="actions-cell">
        <a class="btn-edit" href="edit.html?id=${id}">Edit</a>
      </td>
    `;

        tbody.appendChild(tr);
    });
}

// 5) Prevent HTML injection when inserting text into innerHTML
function escapeHtml(str) {
    return String(str)
        .replaceAll("&", "&amp;")
        .replaceAll("<", "&lt;")
        .replaceAll(">", "&gt;")
        .replaceAll('"', "&quot;")
        .replaceAll("'", "&#039;");
}

// 7) Wire search input to reload brands
document.addEventListener("DOMContentLoaded", () => {
    const searchBox = $("searchInput");
    if (!searchBox) return; // safety if input not exists

    // Live search while typing (with debounce so we don't spam the API)
    let timer = null;

    searchBox.addEventListener("input", () => {
        clearTimeout(timer);

        timer = setTimeout(() => {
            loadBrands(searchBox.value);
        }, 350);
    });
});
