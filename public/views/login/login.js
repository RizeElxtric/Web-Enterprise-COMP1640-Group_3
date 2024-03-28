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
function updateUIAfterLogin() {
  // Tắt hoặc ẩn form đăng nhập
  document.getElementById("loginForm").style.display = "none";

  // Hiển thị hộp nhập bình luận
  document.getElementById("commentBox").style.display = "block";

  // Hoặc chuyển hướng đến trang chính
  // window.location.href = '/home';
}

// Sử dụng hàm này sau khi đăng nhập thành công
fetch("/api/login", {
  method: "POST",
  body: JSON.stringify({
    email: "example@fpt.edu.vn",
    password: "user_password",
  }),
  headers: {
    "Content-Type": "application/json",
  },
})
  .then((response) => {
    if (response.ok) {
      return response.json();
    }
    throw new Error("Error in network response");
  })
  .then((data) => {
    // Đăng nhập thành công
    if (data.status === "success") {
      updateUIAfterLogin();
    } else {
      // Hiện thông báo lỗi
      document.getElementById("message").innerText = data.message;
    }
  })
  .catch((error) => console.error("Error:", error));

// Khởi tạo một tài khoản tạm thời
var tempAccount = {
  email: "temp@fpt.edu.vn",
  password: "123",
};
function updateUIAfterLogin() {
  document.getElementById("loginForm").style.display = "none";
  document.getElementById("commentBox").style.display = "block";
}
// Lưu tài khoản tạm thời vào localStorage
localStorage.setItem("tempAccount", JSON.stringify(tempAccount));
