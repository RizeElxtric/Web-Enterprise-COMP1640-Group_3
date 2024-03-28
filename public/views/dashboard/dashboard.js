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
