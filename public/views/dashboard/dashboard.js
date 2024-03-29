window.onload = function () {
  checkLoginStatus();
  loadEvent();

  // Hiển thị token ra console
  var token = localStorage.getItem("token");
  console.log("Token:", token);
};

//Thay đổi hàm checkLoginStatus
function checkLoginStatus() {
  var isLoggedIn = localStorage.getItem("isLoggedIn");

  if (isLoggedIn) {
    document.getElementById("loginListItem").innerHTML =
      '<a href="../logout/logout.html">Logout</a>';
    loggedInUser = JSON.parse(localStorage.getItem("loggedInUser")); //Lấy thông tin người dùng đã đăng nhập từ localStorage
  } else {
    document.getElementById("loginListItem").innerHTML =
      '<a href="../login/login.html">Login</a>';
  }
}
