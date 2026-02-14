async function loadBrands() {
    const brandSelect = $("brandId");
    brandSelect.innerHTML = `<option value="">Loading...</option>`;

    try {
        const brands = await apiGetJson(BRANDS_API);

        brandSelect.innerHTML =
            `<option value="">-- No Brand --</option>` +
            brands.map(b => `<option value="${b.id ?? b.ID}">${escapeHtml(b.name ?? b.Name)}</option>`).join("");
    } catch {
        brandSelect.innerHTML = `<option value="">Failed to load</option>`;
    }
}

async function loadCategories(selectId = null) {
    const catSelect = $("categoryId");
    catSelect.innerHTML = `<option value="">Loading...</option>`;

    try {
        const categories = await apiGetJson(CATEGORIES_API);

        catSelect.innerHTML =
            `<option value="">-- Select Category --</option>` +
            categories.map(c => `<option value="${c.id ?? c.ID}">${escapeHtml(c.name ?? c.Name)}</option>`).join("");

        if (selectId != null) catSelect.value = String(selectId);
    } catch {
        catSelect.innerHTML = `<option value="">Failed to load</option>`;
    }
}

function openCategoryModal() {
    setMsg("categoryMsg", "");
    $("newCategoryName").value = "";
    $("categoryModal").classList.remove("hidden");
    $("newCategoryName").focus();
}

function closeCategoryModal() {
    $("categoryModal").classList.add("hidden");
}

async function createCategory() {
  const name = $("newCategoryName").value.trim();
  const description = $("newCategoryDesc").value.trim();

  if (!name) {
    setMsg("categoryMsg", "Category name is required.", true);
    return;
  }

  setMsg("categoryMsg", "Creating...");

  try {
    // Use PascalCase to be safe with strict JSON settings
    const created = await apiSendJson(CATEGORIES_API, "POST", { Name: name, Description: description });

    // created might be:
    // - JSON object { id/ID }
    // - text
    // - null (204)
    const createdId = created?.id ?? created?.ID ?? null;

    await loadCategories(createdId); // if null => refresh only
    closeCategoryModal();
    setMsg("pageMsg", "Category created and selected.");
  } catch (e) {
    console.error(e);
    setMsg("categoryMsg", e.message || "Failed to create category.", true);
  }
}

async function updateProduct() {
    setMsg("pageMsg", "");

    const productId = getQueryParam("id");
    if (!productId) {
        setMsg("pageMsg", "Invalid product ID.", true);
        return;
    }

    const name = $("productName").value.trim();
    const capacity = Number($("capacity").value);
    const salePrice = Number($("salePrice").value);
    const costPrice = Number($("costPrice").value);

    const minStockRaw = $("minStock").value;
    const brandRaw = $("brandId").value;
    const categoryRaw = $("categoryId").value;

    if (
        !name ||
        isNaN(capacity) || capacity <= 0 ||
        isNaN(salePrice) || salePrice < 0 ||
        isNaN(costPrice) || costPrice < 0 ||
        !categoryRaw
    ) {
        setMsg("pageMsg", "Please fill all required fields correctly.", true);
        return;
    }

    const payload = {
        name,
        capacity,
        salePrice,
        costPrice,
        minStock: minStockRaw ? Number(minStockRaw) : null,
        brandId: brandRaw ? Number(brandRaw) : null,
        productCategoryId: Number(categoryRaw)
    };

    setMsg("pageMsg", "Saving...");

    try {
        await apiSendJson(`${PRODUCTS_API}/${productId}`, "PATCH", payload);
        setMsg("pageMsg", "Product updated successfully.");
    } catch (e) {
        setMsg("pageMsg", "Failed to update product.", true);
    }
}

document.addEventListener("DOMContentLoaded", async () => {
    await Promise.all([loadBrands(), loadCategories()]);

    $("btnAddCategory").addEventListener("click", openCategoryModal);
    $("btnCloseCategoryModal").addEventListener("click", closeCategoryModal);
    $("btnCancelCategory").addEventListener("click", closeCategoryModal);
    $("btnCreateCategory").addEventListener("click", createCategory);

    // Close when clicking the overlay background
    $("categoryModal").addEventListener("click", (e) => {
        if (e.target.id === "categoryModal") closeCategoryModal();
    });

    // Save
    $("btnSave").addEventListener("click", updateProduct);
});