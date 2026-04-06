// API base for this module
const BRANDS_API = window.API_ENDPOINTS.brands;

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