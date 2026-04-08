const PRODUCTS_API = window.API_ENDPOINTS.products;
const BRANDS_API = window.API_ENDPOINTS.brands;
const CATEGORIES_API = window.API_ENDPOINTS.productCategories; 

document.addEventListener("DOMContentLoaded", () => {
    const searchBox = $("searchInput");
    if (!searchBox) return; // safety if input not exists

    // Live search while typing (with debounce so we don't spam the API)
    let timer = null;

    searchBox.addEventListener("input", () => {
        clearTimeout(timer);

        timer = setTimeout(() => {
            loadProducts(searchBox.value);
        }, 350);
    });
});