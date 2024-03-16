window.onload = function () {
  // Xóa trạng thái 'isLoggedIn' để logout người dùng
  localStorage.removeItem("isLoggedIn");

  // Chuyển hướng người dùng trở lại trang chủ
  window.location.href = "../home/home.html";
};
