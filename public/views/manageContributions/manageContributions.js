function checkLoginStatus() {
  const status = localStorage.getItem("isLoggedIn") ? "Logout" : "Login";
  const link = localStorage.getItem("isLoggedIn")
    ? "../logout/logout.html"
    : "../login/login.html";
  document.getElementById(
    "loginListItem"
  ).innerHTML = `<a href="${link}">${status}</a>`;
}

window.onload = async function () {
  checkLoginStatus();
  await fetchContributions();
};

async function fetchContributions() {
  try {
    const response = await fetch("/contributions");
    const contributions = await response.json();

    displayContributions(contributions);
  } catch (error) {
    console.error("Erro:", error);
  }
}

function createContributionRow(contribution) {
  return `
    <tr>
      <td>${contribution.title}</td>
      <td>${contribution.description}</td>
      <td>${contribution.contributor}</td>
      <td>${contribution.date}</td>
      <td>${contribution.status}</td>
      <td>
        <button onclick="approveContribution('${contribution._id}')">Approve</button>
        <button onclick="rejectContribution('${contribution._id}')">Reject</button>
      </td>
    </tr>
  `;
}

function displayContributions(contributions) {
  const contributionsTable = document.getElementById("contributions-table");
  contributionsTable.innerHTML = contributions
    .map(createContributionRow)
    .join("");
}

async function approveContribution(id) {
  try {
    const response = await fetch(`/contributions/${id}`, {
      method: "PATCH",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ status: "approved" }),
    });
    const result = await response.json();
    console.log(`Approved contribution with id: ${id}`);
    fetchContributions(); // Refresh contributions after approving
  } catch (error) {
    console.error(`Error approving contribution with id ${id}:`, error);
  }
}

async function rejectContribution(id) {
  try {
    const response = await fetch(`/contributions/${id}`, {
      method: "PATCH",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ status: "rejected" }),
    });
    const result = await response.json();
    console.log(`Rejected contribution with id: ${id}`);
    fetchContributions(); // Refresh contributions after rejecting
  } catch (error) {
    console.error(`Error rejecting contribution with id ${id}:`, error);
  }
}
