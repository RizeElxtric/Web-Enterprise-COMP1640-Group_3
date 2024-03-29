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

function loadEvent() {
  fetch("https://marketingeventgroup4.azurewebsites.net/event", {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + localStorage.getItem("token"),
    },
  })
    .then((response) => response.json())
    .then((data) => {
      //Hiển thị thông tin sự kiện sau khi có dữ liệu từ API
      document.getElementById("name").innerText = data.name;
      document.getElementById("description").innerText = data.description;
      document.getElementById("firstClosureDate").innerText =
        "Ngày đóng cửa đầu tiên: " + data.firstClosureDate;
      document.getElementById("finalClosureDate").innerText =
        "Ngày đóng cửa cuối cùng: " + data.finalClosureDate;
    })
    .catch((error) => console.log("Có lỗi xảy ra: " + error));

  //Giả sử người dùng này đã đăng nhập
  var loggedInUser = {
    id: "12345",
    name: "Nguyen Van A",
  };

  let newEvent = localStorage.getItem("newEvent");

  if (newEvent) {
    newEvent = JSON.parse(newEvent);

    document.getElementById("name").innerText = newEvent.name;
    document.getElementById("description").innerText = newEvent.description;

    //Convert ISO strings to date and format it
    const firstDate = new Date(newEvent.firstClosureDate);
    const finalDate = new Date(newEvent.finalClosureDate);

    document.getElementById("firstClosureDate").innerText =
      "Deadline: " + firstDate.toLocaleDateString();
    document.getElementById("finalClosureDate").innerText =
      "Deadline: " + finalDate.toLocaleDateString();

    // Remove the event data from localStorage after using it
    localStorage.removeItem("newEvent");
  }
}
