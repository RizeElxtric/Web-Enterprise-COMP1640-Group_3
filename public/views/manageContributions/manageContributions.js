// Hàm kiểm tra trạng thái đăng nhập
function checkLoginStatus() {
  if (localStorage.getItem("isLoggedIn")) {
    document.getElementById("loginListItem").innerHTML =
      '<a href="../logout/logout.html">Logout</a>';
  } else {
    document.getElementById("loginListItem").innerHTML =
      '<a href="../login/login.html">Login</a>';
  }
}

// Gọi hàm kiểm tra trạng thái đăng nhập khi trang web tải xong
window.onload = checkLoginStatus;

// window.onload = function () {
//   fetchContributions();
// };

// function fetchContributions() {
//   // Giả sử chúng ta có API end-point để lấy ra danh sách các contributions
//   fetch("/api/contributions")
//     .then((response) => response.json())
//     .then((contributions) => displayContributions(contributions))
//     .catch((error) => console.error("Error:", error));
// }

// function displayContributions(contributions) {
//   const contributionsTable = document.getElementById("contributions-table");
//   contributions.forEach((contribution) => {
//     contributionsTable.innerHTML += `
//             <tr>
//                 <td>${contribution.title}</td>
//                 <td>${contribution.description}</td>
//                 <td>${contribution.contributor}</td>
//                 <td>${contribution.date}</td>
//                 <td>${contribution.status}</td>
//                 <td>
//                     <button onclick="approveContribution(${contribution.id})">Approve</button>
//                     <button onclick="rejectContribution(${contribution.id})">Reject</button>
//                 </td>
//             </tr>
//         `;
//   });
// }

// function approveContribution(id) {
//   // Logic để chấp nhận một contribution
// }

// function rejectContribution(id) {
//   // Logic để từ chối một contribution
// }
