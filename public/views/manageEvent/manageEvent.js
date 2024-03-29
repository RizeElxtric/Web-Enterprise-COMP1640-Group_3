// // Thay đổi hàm checkLoginStatus
// function checkLoginStatus() {
//   var isLoggedIn = localStorage.getItem("isLoggedIn");

//   if (isLoggedIn) {
//     document.getElementById("loginListItem").innerHTML =
//       '<a href="../logout/logout.html">Logout</a>';
//     loggedInUser = JSON.parse(localStorage.getItem("loggedInUser")); // Lấy thông tin người dùng đã đăng nhập từ localStorage
//   } else {
//     document.getElementById("loginListItem").innerHTML =
//       '<a href="../login/login.html">Login</a>';
//   }
// }
// // Gọi hàm kiểm tra trạng thái đăng nhập khi trang web tải xong

// window.onload = function () {
//   checkLoginStatus();

//   document
//     .getElementById("eventForm")
//     .addEventListener("submit", function (event) {
//       event.preventDefault();

//       let eventInfo = {
//         name: document.getElementById("eventName").value,
//         description: document.getElementById("eventDescription").value,
//         firstClosureDate: new Date(
//           document.getElementById("firstClosureDate").value
//         ).toISOString(),
//         finalClosureDate: new Date(
//           document.getElementById("finalClosureDate").value
//         ).toISOString(),
//       };

//       fetch("https://marketingeventgroup4.azurewebsites.net/event", {
//         method: "POST",
//         headers: {
//           "Content-Type": "application/json",
//           Authorization: "Bearer " + localStorage.getItem("token"),
//         },
//         body: JSON.stringify(eventInfo),
//       })
//         .then((response) => {
//           if (!response.ok) {
//             return response.text().then((text) => {
//               throw new Error(text);
//             });
//           }
//           return response.json();
//         })
//         .then((data) => {
//           console.log("Sự kiện đã được tạo thành công: ", data);
//           //   window.location.href = "../home/home.html"; // Thay thế bằng URL trang thành công của bạn
//         })
//         .catch((error) => console.log("Có lỗi xảy ra: ", error));
//     });
// };
//--------------------------------------------------------------------------

document.addEventListener("DOMContentLoaded", function () {
  const addEventButton = document.getElementById("add-event-button");
  const modal = document.getElementById("add-event-modal");
  const closeButton = modal.querySelector(".close");
  const form = document.getElementById("add-event-form");
  const eventList = document.getElementById("event-list");
  let formSubmitted = false; // Biến để kiểm tra form đã được submit hay chưa

  addEventButton.addEventListener("click", function () {
    if (!formSubmitted) {
      // Kiểm tra xem form đã được submit hay chưa
      modal.style.display = "block";
    }
  });

  closeButton.addEventListener("click", function () {
    modal.style.display = "none";
  });

  window.addEventListener("click", function (event) {
    if (event.target === modal) {
      modal.style.display = "none";
    }
  });

  form.addEventListener("submit", function (event) {
    event.preventDefault();
    formSubmitted = true; // Đánh dấu form đã được submit
    const name = document.getElementById("event-name").value;
    const description = document.getElementById("event-description").value;
    const date = document.getElementById("event-date").value;
    const eventItem = createEventItem(name, description, date);
    eventList.appendChild(eventItem);
    modal.style.display = "none";
    form.reset();
  });

  function createEventItem(name, description, date) {
    const eventItem = document.createElement("div");
    eventItem.classList.add("event");
    eventItem.innerHTML = `
        <h2>${name}</h2>
        <p>${description}</p>
        <p>Date: ${date}</p>
      `;
    return eventItem;
  }
});
