document.getElementById("loginButton").addEventListener("click", function () {
  document.getElementById("loginForm").style.display = "block";
});

document.getElementById("submitEmail").addEventListener("click", function () {
  var email = document.getElementById("email").value;
  var allowedDomain = "@fpt.edu.vn"; // Domain của FPT Email

  if (email.endsWith(allowedDomain)) {
    document.getElementById("message").innerText = "Login successful!";
    // Đặt trạng thái đã đăng nhập
    localStorage.setItem("isLoggedIn", true);
    // Thực hiện hành động sau khi đăng nhập thành công, ví dụ: điều hướng đến trang chính
    window.location.href = "../home/home.html"; // điều hướng đến trang home
  } else {
    document.getElementById("message").innerText =
      "Please enter a valid FPT email address";
  }
});

document.getElementById("loginButton").addEventListener("click", function () {
  document.getElementById("loginForm").style.display = "block";
});
