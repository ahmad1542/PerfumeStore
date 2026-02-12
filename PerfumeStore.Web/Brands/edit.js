let brandId = null;

async function loadBrandForEdit() {
    brandId = getQueryParam("id");
    if (!brandId) {
        setMsg("pageMsg", "Missing brand id in URL.", true);
        return;
    }

    setMsg("pageMsg", "Loading brand...");

    const res = await fetch(`${BRANDS_API}/${encodeURIComponent(brandId)}`, {
        headers: { Accept: "application/json" }
    });

    if (!res.ok) {
        setMsg("pageMsg", `Failed to load brand (HTTP ${res.status}).`, true);
        return;
    }

    const brand = await res.json();

    $("brandName").value = brand.name ?? brand.Name ?? "";
    $("brandDesc").value = brand.brandDescription ?? brand.BrandDescription ?? "";
    setMsg("pageMsg", "");
}

async function saveBrandEdits() {
    if (!brandId) return;

    const name = $("brandName").value.trim();
    const desc = $("brandDesc").value.trim();

    if (!name) {
        setMsg("pageMsg", "Name is required", true);
        return;
    }

    setMsg("pageMsg", "Saving...");

    const res = await fetch(`${BRANDS_API}/${encodeURIComponent(brandId)}`, {
        method: "PATCH",
        headers: {
            "Content-Type": "application/json",
            Accept: "application/json"
        },
        body: JSON.stringify({
            id: brandId,
            name: name,
            BrandDescription: desc
        })
    });

    if (!res.ok) {
        setMsg("pageMsg", `Update failed (HTTP ${res.status}).`, true);
        return;
    }

    setMsg("pageMsg", "Brand updated successfully.");
}