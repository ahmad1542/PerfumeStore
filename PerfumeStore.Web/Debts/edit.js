async function initPage() {
  try {
    await fillDebtPartiesSelect("PersonId");
    await fillMoneyAccounts("moneyAccount");
  } catch (e) {
    console.error(e);
  }
}

async function fillMoneyAccounts(selectId) {
  const el = $(selectId);
  if (!el) return;
  el.innerHTML = `<option value="">-- Select Account --</option>`;
  let list = [];
  try { list = await apiGetJson(window.API_ENDPOINTS.moneyAccounts); } catch { list = []; }
  (Array.isArray(list) ? list : []).forEach(a => {
    const id = a.id ?? a.ID ?? a.Id ?? "";
    const name = a.accountName ?? a.AccountName ?? ("Account #" + id);
    if (id === "") return;
    const opt = document.createElement("option");
    opt.value = id;
    opt.textContent = name;
    el.appendChild(opt);
  });
}

async function fillDebtPartiesSelect(selectId) {
  const el = $(selectId);
  if (!el) return;

  el.innerHTML = `<option value="">-- Select Person / Supplier --</option>`;

  let persons = [];
  let suppliers = [];

  try { persons = await apiGetJson(window.API_ENDPOINTS.persons); } catch { persons = []; }
  try { suppliers = await apiGetJson(window.API_ENDPOINTS.suppliers); } catch { suppliers = []; }

  const seen = new Set();
  const items = [];

  (Array.isArray(persons) ? persons : []).forEach((p) => items.push({ ...p, _partyType: "Person" }));
  (Array.isArray(suppliers) ? suppliers : []).forEach((s) => items.push({ ...s, _partyType: "Supplier" }));

  items.forEach((p) => {
    const id = p.id ?? p.ID ?? p.Id ?? "";
    if (!id || seen.has(id)) return;
    seen.add(id);

    const name = p.name ?? p.Name ?? "";
    const phone = p.phone ?? p.Phone ?? "";
    const type = p._partyType || "Person";

    const opt = document.createElement("option");
    opt.value = id;
    opt.textContent = `${type} - ${name ? `${name}${phone ? ` (${phone})` : ""}` : (phone || id)}`;
    el.appendChild(opt);
  });
}

function buildBodyFromForm() {
  const date = $("Date")?.value || null;
  const amount = Number($("Amount")?.value || 0);
  const personIdRaw = $("PersonId")?.value?.trim() || "";

  return {
    Date: date,
    Amount: amount,
    moneyAccountId: parseInt($("moneyAccount")?.value),
    direction: parseInt($("direction")?.value),
    PersonId: personIdRaw || null,
    Notes: $("Notes")?.value || null
  };
}

function validateBody(body) {
  if (!body.Date) return "Date is required.";
  if (!body.Amount || body.Amount <= 0) return "Amount is required and must be greater than 0.";
  if (!body.PersonId) return "Please select a Person / Supplier.";
  if (!body.moneyAccountId || isNaN(body.moneyAccountId)) return "Money Account is required.";
  if (!body.direction || isNaN(body.direction)) return "Direction is required.";

  return null;
}

async function initEdit() {
  const id = getQueryParam("id");
  if (!id) {
    setMsg("pageMsg", "Missing id in URL.", true);
    return;
  }

  if (typeof initPage === "function") await initPage();

  setMsg("pageMsg", "Loading...", false);

  try {
    const item = await apiGetJson(`${API}/${encodeURIComponent(id)}`);

    const date = item.date ?? item.Date ?? "";
    const amount = item.amount ?? item.Amount ?? "";
    const personId = item.personId ?? item.PersonId ?? item.personid ?? "";
    const notes = item.notes ?? item.Notes ?? "";
    const direction = item.direction ?? item.Direction ?? "";
    const moneyAccountId = item.moneyAccountId ?? item.MoneyAccountId ?? item.moneyaccountid ?? "";

    if ($("Date")) {
      $("Date").value = date ? String(date).split("T")[0] : "";
    }
    if ($("Amount")) $("Amount").value = amount;
    if ($("PersonId")) $("PersonId").value = personId ?? "";
    if ($("Notes")) $("Notes").value = notes ?? "";
    if ($("direction")) $("direction").value = String(direction ?? "");
    if ($("moneyAccount")) $("moneyAccount").value = String(moneyAccountId ?? "");

    setMsg("pageMsg", "");
  } catch (e) {
    const validationMsg = getFriendlyMessage(e);

    setMsg(
      "pageMsg",
      validationMsg ?? "Failed to load record from API.",
      true
    );
    console.error(e);
    return;
  }

  const btn = $("btnSave");
  if (btn) btn.addEventListener("click", () => updateItem(id));
}

async function updateItem(id) {
  setMsg("pageMsg", "Saving...", false);
  try {
    const body = buildBodyFromForm();
    const err = validateBody(body);
    if (err) {
      setMsg("pageMsg", err, true);
      return;
    }

    await apiSendJson(`${API}/${encodeURIComponent(id)}`, "PATCH", body);
    window.location.href = "index.html";
  } catch (e) {
    const validationMsg = getFriendlyMessage(e);

    setMsg(
      "pageMsg",
      validationMsg ?? "Update failed. Check API and payload.",
      true
    );
    console.error(e);
  }
}