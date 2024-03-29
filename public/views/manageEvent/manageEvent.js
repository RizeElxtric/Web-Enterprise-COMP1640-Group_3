// Thay đổi hàm checkLoginStatus
function checkLoginStatus() {
  var isLoggedIn = localStorage.getItem("isLoggedIn");

  if (isLoggedIn) {
    document.getElementById("loginListItem").innerHTML =
      '<a href="../logout/logout.html">Logout</a>';
    loggedInUser = JSON.parse(localStorage.getItem("loggedInUser")); // Lấy thông tin người dùng đã đăng nhập từ localStorage
  } else {
    document.getElementById("loginListItem").innerHTML =
      '<a href="../login/login.html">Login</a>';
  }
}
// Gọi hàm kiểm tra trạng thái đăng nhập khi trang web tải xong

window.onload = function () {
  checkLoginStatus();

  document
    .getElementById("eventForm")
    .addEventListener("submit", function (event) {
      event.preventDefault();

      let eventInfo = {
        name: document.getElementById("eventName").value,
        description: document.getElementById("eventDescription").value,
        firstClosureDate: new Date(
          document.getElementById("firstClosureDate").value
        ).toISOString(),
        finalClosureDate: new Date(
          document.getElementById("finalClosureDate").value
        ).toISOString(),
      };

      fetch("https://marketingeventgroup4.azurewebsites.net/event", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + localStorage.getItem("token"),
        },
        body: JSON.stringify(eventInfo),
      })
        .then((response) => {
          if (!response.ok) {
            return response.text().then((text) => {
              throw new Error(text);
            });
          }
          return response.json();
        })
        .then((data) => {
          console.log("Sự kiện đã được tạo thành công: ", data);
          //   window.location.href = "../home/home.html"; // Thay thế bằng URL trang thành công của bạn
        })
        .catch((error) => console.log("Có lỗi xảy ra: ", error));
    });
};
