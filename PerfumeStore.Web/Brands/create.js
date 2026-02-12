async function addBrand() {
    const name = $("brandName").value.trim();
    const desc = $("brandDesc").value.trim();

    if (!name) {
        setMsg("pageMsg", "Name is required.", true);
        return;
    }

    setMsg("pageMsg", "Saving...");

    const res = await fetch(BRANDS_API, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Accept: "application/json"
        },
        body: JSON.stringify({
            name: name,
            description: desc
        })
    });

    if (!res.ok) {
        setMsg("pageMsg", `Create failed (HTTP ${res.status}).`, true);
        return;
    }

    setMsg("pageMsg", "Brand created successfully.");
}