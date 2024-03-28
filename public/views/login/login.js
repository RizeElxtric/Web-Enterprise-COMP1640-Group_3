document.addEventListener("DOMContentLoaded", function () {
  document
    .getElementById("loginForm")
    .addEventListener("submit", function (event) {
      event.preventDefault();

      var username = document.getElementById("username").value;
      var password = document.getElementById("password").value;

      console.log("Sending:", { username: username, password: password }); // Ghi dữ liệu cần gửi vào console

      fetch("https://marketingeventgroup4.azurewebsites.net/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          username: username,
          password: password,
        }),
      })
        .then((response) => {
          if (response.ok) {
            return response.text(); // Sử dụng .text() thay vì .json()
          } else {
            throw new Error("Đăng nhập không thành công");
          }
        })
        .then((data) => {
          try {
            data = JSON.parse(data); // Cố gắng phân tích chuỗi thành JSON
          } catch (error) {
            console.error("Could not parse data as JSON:", data);
          }
          console.log("Received:", data);
          document.getElementById("message").textContent =
            "Đăng nhập thành công!";
          window.location.href = "../home/home.html";
        })
        .catch((error) => {
          console.error("Error:", error); // Ghi lỗi vào console
          document.getElementById("message").textContent =
            "Đăng nhập không thành công. Vui lòng kiểm tra lại thông tin đăng nhập.";
        });
    });
});
