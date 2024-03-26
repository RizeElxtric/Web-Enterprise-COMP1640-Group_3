let userContributions = [];
function checkLoginStatus() {
  const status = localStorage.getItem("isLoggedIn") ? "Logout" : "Login";
  const link = localStorage.getItem("isLoggedIn")
    ? "../logout/logout.html"
    : "../login/login.html";
  document.getElementById(
    "loginListItem"
  ).innerHTML = `<a href="${link}">${status}</a>`;
}

function displayUserContribution(contribution) {
  const container = document.getElementById("contributions-table");
  const contributionElement = document.createElement("div");
  contributionElement.innerText = `${contribution.title}: ${contribution.description}`;
  container.appendChild(contributionElement);
}

async function submitUserInputs(event) {
  event.preventDefault();

  const titleElement = document.getElementById("title");
  const descriptionElement = document.getElementById("description");

  if (titleElement && descriptionElement) {
    const title = titleElement.value;
    const description = descriptionElement.value;

    if (title && description) {
      const newContribution = {
        title: title,
        description: description,
        contributor: localStorage.getItem("username") || "User", // Lấy tên người dùng từ localStorage
        date: new Date().toISOString().split("T")[0],
        status: "pending",
      };

      try {
        const response = await fetch("/contributions", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(newContribution),
        });

        if (response.ok) {
          alert("Contribution submitted successfully");
          displayUserContribution(newContribution);
          titleElement.value = "";
          descriptionElement.value = "";
        } else {
          throw new Error("Failed to submit contribution");
        }
      } catch (error) {
        console.error("Error:", error);
        alert("Failed to submit contribution");
      }
    } else {
      alert("Please fill in both title and description.");
    }
  } else {
    alert("Please input title and description.");
  }
}

window.onload = function () {
  checkLoginStatus();
  document
    .getElementById("contribute-form")
    .addEventListener("submit", submitUserInputs);
};
