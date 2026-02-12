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

function setCategoryMsg(text, isError = false) {
    const el = document.getElementById("categoryMsg");
    if (!el) return;
    el.textContent = text || "";
    el.style.color = isError ? "#ff8b8b" : "rgba(232,238,252,.65)";
}

function openCategoryModal() {
    setCategoryMsg("categoryMsg", "");
    document.getElementById("newCategoryName").value = "";
    document.getElementById("categoryModal").classList.remove("hidden");
    document.getElementById("newCategoryName").focus();
}

function closeCategoryModal() {
    document.getElementById("categoryModal").classList.add("hidden");
}

async function addProduct() {
    const name = $("productName").value.trim();
    const capacity = $("capacity").value.trim();
    const salePrice = $("salePrice").value.trim();
    const costPrice = $("costPrice").value.trim();
    const minStock = $("minStock").value.trim();

    if (!name) {
        setMsg("Name is required.", true);
        return;
    }

    if (!capacity) {
        setMsg("Capacity is required.", true);
        return;
    }

    if (!salePrice) {
        setMsg("Sale Price is required.", true);
        return;
    }

    if (!costPrice) {
        setMsg("Cost Price is required.", true);
        return;
    }

    setMsg("Saving...");

    const res = await fetch(PRODUCTS_API, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Accept: "application/json"
        },
        body: JSON.stringify({
            name: name,
            capacity: capacity,
            salePrice: salePrice,
            costPrice: costPrice,
            minStock: minStock
        })
    });

    if (!res.ok) {
        setMsg(`Create failed (HTTP ${res.status}).`, true);
        return;
    }

    setMsg("Product created successfully.");
}