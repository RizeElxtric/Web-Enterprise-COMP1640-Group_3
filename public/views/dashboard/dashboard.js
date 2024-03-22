// Kiểm tra trạng thái đăng nhập
function checkLoginStatus() {
  if (localStorage.getItem("isLoggedIn")) {
    document.getElementById("loginListItem").innerHTML =
      '<a href="../logout/logout.html">Logout</a>';
  } else {
    document.getElementById("loginListItem").innerHTML =
      '<a href="../login/login.html">Login</a>';
  }
}

document.addEventListener("DOMContentLoaded", async function () {
  checkLoginStatus();

  var contributionsList = document.getElementById("contributionList");

  var response = await fetch("http://localhost:8000/contributions");
  var contributions = await response.json();

  contributions.forEach(function (contribution) {
    var div = document.createElement("div");

    var h2 = document.createElement("h2");
    h2.textContent = contribution.title;

    var p = document.createElement("p");
    p.textContent = contribution.description;

    div.appendChild(h2);
    div.appendChild(p);

    contributionsList.appendChild(div);
  });
});
