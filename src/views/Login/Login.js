document.getElementById("loginButton").addEventListener("click", function () {
  document.getElementById("loginForm").style.display = "block";
});

document.getElementById("submitEmail").addEventListener("click", function () {
  var email = document.getElementById("email").value;
  var allowedDomain = "@fpt.edu.vn"; // Domain của FPT Email

  if (email.endsWith(allowedDomain)) {
    document.getElementById("message").innerText = "Login successful!";
    // Thực hiện hành động sau khi đăng nhập thành công, ví dụ: điều hướng đến trang chính
  } else {
    document.getElementById("message").innerText =
      "Please enter a valid FPT email address";
  }
});
