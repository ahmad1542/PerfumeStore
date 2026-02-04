// Sidebar toggle (mobile)
const sidebar = document.getElementById('sidebar');
const sidebarBackdrop = document.getElementById('sidebar-backdrop');
const toggleBtn = document.getElementById('toggleSidebar');

toggleBtn?.addEventListener('click', () => {
  sidebar.classList.toggle('open');
  sidebarBackdrop.style.display = sidebar.classList.contains('open') ? 'block' : 'none';
});

sidebarBackdrop?.addEventListener('click', () => {
  sidebar.classList.remove('open');
  sidebarBackdrop.style.display = 'none';
});



// Fake data (replace with API later)
const dashboardData = {
  kpis: {
    sales: { value: 1840, deltaPct: 12.4 },
    purchases: { value: 920, deltaPct: -3.1 },
    earnings: { value: 560, deltaPct: 7.6 },
    cash: { value: 1350 }
  },
  daily: {
    labels: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23', '24', '25', '26', '27', '28', '29', '30'],
    sales: [40, 60, 55, 80, 30, 90, 100, 75, 60, 40, 55, 70, 90, 65, 50, 100, 110, 85, 75, 60, 65, 70, 95, 88, 60, 62, 78, 85, 92, 100],
    purchases: [20, 35, 30, 45, 25, 50, 60, 40, 38, 25, 30, 35, 44, 40, 30, 55, 58, 45, 42, 38, 35, 40, 50, 48, 35, 34, 40, 44, 46, 52]
  },
  profit: {
    labels: ['Week 1', 'Week 2', 'Week 3', 'Week 4'],
    values: [120, 150, 130, 160]
  },
  brands: [
    { name: 'Dior', value: 420 },
    { name: 'Chanel', value: 360 },
    { name: 'Tom Ford', value: 290 },
    { name: 'YSL', value: 260 },
    { name: 'Versace', value: 210 }
  ],
  expenses: [
    { name: 'Rent', value: 180 },
    { name: 'Shipping', value: 70 },
    { name: 'Ads', value: 55 },
    { name: 'Supplies', value: 40 },
    { name: 'Other', value: 35 }
  ],
  alerts: [
    { icon: 'bi-exclamation-circle', title: 'Customer debts due soon', desc: '3 invoices due in the next 7 days', tone: 'warning' },
    { icon: 'bi-truck', title: 'Supplier payment needed', desc: '1 supplier voucher pending', tone: 'danger' },
    { icon: 'bi-archive', title: 'Low stock items', desc: '5 products below threshold', tone: 'info' }
  ],
  activity: [
    { date: '2026-02-03', type: 'Sale Invoice', ref: '#SI-1042', party: 'Ahmad', amount: 85, status: 'Paid' },
    { date: '2026-02-03', type: 'Receipt Voucher', ref: '#RV-331', party: 'Mahmoud', amount: 120, status: 'Received' },
    { date: '2026-02-02', type: 'Purchase Invoice', ref: '#PI-220', party: 'Supplier A', amount: 260, status: 'Unpaid' },
    { date: '2026-02-01', type: 'Expense', ref: '#EX-98', party: 'Rent', amount: 180, status: 'Booked' }
  ]
};

// Helpers
const fmtMoney = (n) => '$' + (n ?? 0).toLocaleString(undefined, { maximumFractionDigits: 2 });
const setDelta = (elId, pct) => {
  const el = document.getElementById(elId);
  if (!el) return;
  const up = pct >= 0;
  el.classList.toggle('up', up);
  el.classList.toggle('down', !up);
  el.innerHTML = up
    ? `<i class="bi bi-arrow-up-right"></i> +${pct.toFixed(1)}%`
    : `<i class="bi bi-arrow-down-right"></i> ${pct.toFixed(1)}%`;
};

// KPI binding
document.getElementById('salesValue').textContent = fmtMoney(dashboardData.kpis.sales.value);
document.getElementById('purchasesValue').textContent = fmtMoney(dashboardData.kpis.purchases.value);
document.getElementById('earningsValue').textContent = fmtMoney(dashboardData.kpis.earnings.value);
document.getElementById('cashValue').textContent = fmtMoney(dashboardData.kpis.cash.value);

setDelta('salesDelta', dashboardData.kpis.sales.deltaPct);
setDelta('purchasesDelta', dashboardData.kpis.purchases.deltaPct);
setDelta('earningsDelta', dashboardData.kpis.earnings.deltaPct);

// Alerts
const alertsWrap = document.getElementById('alertsList');
const toneMap = { info: 'text-info', warning: 'text-warning', danger: 'text-danger' };
dashboardData.alerts.forEach(a => {
  const div = document.createElement('div');
  div.className = 'p-3 rounded-4 border';
  div.style.borderColor = 'rgba(255,255,255,.10)';
  div.style.background = 'rgba(255,255,255,.04)';
  div.innerHTML = `
        <div class="d-flex gap-2 align-items-start">
          <i class="bi ${a.icon} ${toneMap[a.tone] || ''}" style="font-size:1.15rem;"></i>
          <div>
            <div style="font-weight:800;">${a.title}</div>
            <div style="color:rgba(232,238,252,.65); font-size:.9rem;">${a.desc}</div>
          </div>
        </div>
      `;
  alertsWrap.appendChild(div);
});

// Recent activity rows
const tbody = document.getElementById('activityRows');
dashboardData.activity.forEach(r => {
  const tr = document.createElement('tr');
  tr.innerHTML = `
        <td>${r.date}</td>
        <td>${r.type}</td>
        <td>${r.ref}</td>
        <td>${r.party}</td>
        <td class="text-end">${fmtMoney(r.amount)}</td>
        <td><span class="badge text-bg-secondary">${r.status}</span></td>
        <td class="text-end">
          <button class="btnx btn-sm">View</button>
        </td>
      `;
  tbody.appendChild(tr);
});

// Charts
const spCtx = document.getElementById('salesPurchasesChart');
const profitCtx = document.getElementById('profitChart');
const brandCtx = document.getElementById('brandChart');
const expCtx = document.getElementById('expenseChart');

const salesPurchasesChart = new Chart(spCtx, {
  type: 'line',
  data: {
    labels: dashboardData.daily.labels,
    datasets: [
      { label: 'Sales', data: dashboardData.daily.sales, tension: 0.35, borderWidth: 2, pointRadius: 0 },
      { label: 'Purchases', data: dashboardData.daily.purchases, tension: 0.35, borderWidth: 2, pointRadius: 0 }
    ]
  },
  options: {
    responsive: true,
    plugins: { legend: { labels: { color: '#e8eefc' } } },
    scales: {
      x: { ticks: { color: 'rgba(232,238,252,.7)' }, grid: { color: 'rgba(255,255,255,.06)' } },
      y: { ticks: { color: 'rgba(232,238,252,.7)' }, grid: { color: 'rgba(255,255,255,.06)' } }
    }
  }
});

const profitChart = new Chart(profitCtx, {
  type: 'line',
  data: {
    labels: dashboardData.profit.labels,
    datasets: [{ label: 'Profit', data: dashboardData.profit.values, tension: 0.35, borderWidth: 2, pointRadius: 2 }]
  },
  options: {
    responsive: true,
    plugins: { legend: { labels: { color: '#e8eefc' } } },
    scales: {
      x: { ticks: { color: 'rgba(232,238,252,.7)' }, grid: { color: 'rgba(255,255,255,.06)' } },
      y: { ticks: { color: 'rgba(232,238,252,.7)' }, grid: { color: 'rgba(255,255,255,.06)' } }
    }
  }
});

const brandChart = new Chart(brandCtx, {
  type: 'bar',
  data: {
    labels: dashboardData.brands.map(x => x.name),
    datasets: [{ label: 'Sales', data: dashboardData.brands.map(x => x.value), borderWidth: 1 }]
  },
  options: {
    responsive: true,
    plugins: { legend: { labels: { color: '#e8eefc' } } },
    scales: {
      x: { ticks: { color: 'rgba(232,238,252,.7)' }, grid: { color: 'rgba(255,255,255,.06)' } },
      y: { ticks: { color: 'rgba(232,238,252,.7)' }, grid: { color: 'rgba(255,255,255,.06)' } }
    }
  }
});

const expenseChart = new Chart(expCtx, {
  type: 'doughnut',
  data: {
    labels: dashboardData.expenses.map(x => x.name),
    datasets: [{ label: 'Expenses', data: dashboardData.expenses.map(x => x.value) }]
  },
  options: {
    responsive: true,
    plugins: { legend: { labels: { color: '#e8eefc' } } }
  }
});

// Refresh button (demo)
document.getElementById('btnRefresh').addEventListener('click', async () => {
  // Later: fetch('/api/dashboard?month=2026-02')...
  // For now just animate a little change
  dashboardData.kpis.sales.value += Math.round(Math.random() * 50);
  document.getElementById('salesValue').textContent = fmtMoney(dashboardData.kpis.sales.value);
  salesPurchasesChart.update();
});