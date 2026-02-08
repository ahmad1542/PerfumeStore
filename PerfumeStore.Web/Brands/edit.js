let brandId
async function loadBrandForEdit(id) {
    brandId = getQueryParam("id");
    if (!brandId) {
        setMsg("Missing brand id in URL.", true);
        return;
    }

    setMsg("Loading brand...");

    const res = await fetch(`${BRANDS_API}/${encodeURIComponent(brandId)}`, {
        headers: { Accept: "application/json" }
    });

    if (!res.ok) {
        setMsg(`Failed to load brand (HTTP ${res.status}).`, true);
        return;
    }

    const brand = await res.json();

    $("brandNameText").textContent = brand.name ?? brand.Name ?? "-";
    $("brandDescText").textContent = brand.brandDescription ?? brand.BrandDescription ?? "";
    setMsg("");
}

async function saveBrandEdits() {
    if (!brandId) return;

    const name = $("brandName").value.trim();
    const desc = $("brandDesc").value.trim();

    if (!name) {
        setMsg("Name is required", true);
        return;
    }

    setMsg("Saving...");

    const res = await fetch(`${BRANDS_API}/${encodeURIComponent(brandId)}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            Accept: "application/json"
        },
        body: JSON.stringify({
            id: brandId,
            name: name,
            description: desc
        })
    });

    
    if (!res.ok) {
        setMsg(`Update failed (HTTP ${res.status}).`, true);
        return;
    }

    setMsg("Brand updated successfully.");
}