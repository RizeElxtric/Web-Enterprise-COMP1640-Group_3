document.addEventListener("DOMContentLoaded", function () {
  // Add event listener to login button
  document.getElementById("submit-btn").addEventListener("click", function () {
    document.getElementById("loginForm").style.display = "block";
  });
  document.addEventListener("DOMContentLoaded", function () {
    // Fetch data after the page loaded
    fetch("https://api.example.com/userdata", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
    })
      .then((response) => response.json())
      .then((data) => console.log(data))
      .catch((error) => console.log("Có lỗi xảy ra: " + error));

    // Add event listener to login button...
    // Rest of your code...
  });
  // Add event listener to submit button
  document.getElementById("submit-btn").addEventListener("click", login);
});
var token = localStorage.getItem("token");

// Sau đó bạn có thể sử dụng biến token ở đâu đó trong mã nguồn của bạn
console.log(token);
const apiUrl = "https://marketingeventgroup4.azurewebsites.net";

async function login(event) {
  event.preventDefault();

  let data = {
    username: document.getElementById("username").value,
    password: document.getElementById("password").value,
  };

  let response = await fetch(`${apiUrl}/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  if (response.ok) {
    let token = await response.text();
    localStorage.setItem("token", token);
    let payload = token.split(".")[1];
    let decodedPayload = atob(payload);
    let jsonPayload = JSON.parse(decodedPayload);
    localStorage.setItem("role", jsonPayload.role); // Lưu vai trò vào localStorage
    localStorage.setItem("isLoggedIn", true); // Thêm dòng này
    if (localStorage.getItem("isLoggedIn")) {
      window.location.href = "../home/home.html"; // điều hướng người dùng đến trang home
    }
  } else {
    throw new Error("Đăng nhập không thành công");
  }
}
