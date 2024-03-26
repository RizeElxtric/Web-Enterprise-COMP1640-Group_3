let userContributions = [];

function checkLoginStatus() {
  const loginListItem = document.getElementById("loginListItem");

  if (localStorage.getItem("isLoggedIn")) {
    loginListItem.innerHTML = '<a href="../logout/logout.html">Logout</a>';
  } else {
    loginListItem.innerHTML = '<a href="../login/login.html">Login</a>';
  }
}

function attachFormSubmitListener() {
  const contributeForm = document.getElementById("contribute-form");

  contributeForm.addEventListener("submit", async function (event) {
    event.preventDefault();

    let title = document.getElementById("title").value;
    let description = document.getElementById("description").value;

    let newContribution = { title: title, description: description };

    try {
      const response = await fetch("/contributions", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(newContribution), // Changed from 'userContributions' to 'newContribution'
      });

      if (response.ok) {
        userContributions.push(newContribution);
        const data = await response.json();
        console.log(data);
        alert("Contribution submitted successfully");
        contributeForm.reset();
      } else {
        throw new Error("Failed to submit contribution");
      }
    } catch (error) {
      console.error("Error:", error);
      alert("Failed to submit contribution");
    }
  });
}

window.onload = function () {
  checkLoginStatus();
  attachFormSubmitListener();
};
