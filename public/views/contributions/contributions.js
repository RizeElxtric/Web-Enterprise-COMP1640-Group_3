let userContributions = [];

function checkLoginStatus() {
  if (localStorage.getItem("isLoggedIn")) {
    document.getElementById("loginListItem").innerHTML =
      '<a href="./logout.html">Logout</a>';
  } else {
    document.getElementById("loginListItem").innerHTML =
      '<a href="./login.html">Login</a>';
  }
}

window.onload = checkLoginStatus;

document
  .getElementById("contribute-form")
  .addEventListener("submit", async function (event) {
    event.preventDefault();

    var title = document.getElementById("title").value;
    var description = document.getElementById("description").value;

    // Add user's contributions into `userContributions`
    userContributions.push({ title: title, description: description });

    var response = await fetch("/contributions", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(userContributions), // submit `userContributions`
    });

    if (response.ok) {
      var data = await response.json();
      console.log(data);
      alert("Contribution submitted successfully");

      //Reset form and userContributions array after submission
      document.getElementById("contribute-form").reset();
      userContributions = [];
    } else {
      alert("Failed to submit contribution");
      console.error("Error:", error);
    }
  });
