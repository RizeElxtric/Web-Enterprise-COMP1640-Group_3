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
window.onload = checkLoginStatus;

// Giả sử người dùng này đã đăng nhập
var loggedInUser = {
  id: "12345",
  name: "Nguyen Van A",
};

function postComment(i) {
  var commentInput = document.getElementById("commentInput" + i);
  var fileInput = document.getElementById("fileInput" + i);
  var commentList = document.getElementById("commentList" + i);
  var commentText = commentInput.value;

  var commentText = commentInput.value.trim(); // loại bỏ khoảng trắng ở đầu và cuối

  var files = fileInput.files;
  var commentText = commentInput.value;

  var newComment = document.createElement("div");
  newComment.classList.add("comment");

  var userIdText = document.createElement("p");
  userIdText.textContent = "User ID: " + loggedInUser.id;
  newComment.appendChild(userIdText);

  var userNameText = document.createElement("p");
  userNameText.textContent = "User Name: " + loggedInUser.name;
  newComment.appendChild(userNameText);

  var commentContentText = document.createElement("p");
  commentContentText.textContent = commentText;
  newComment.appendChild(commentContentText);

  var timestamp = document.createElement("p");
  timestamp.textContent = "Posted at: " + new Date().toLocaleString();
  newComment.appendChild(timestamp);

  // không cho phép gửi bình luận nếu chưa đăng nhập hoặc không có nội dung
  // if (!localStorage.getItem("isLoggedIn")) {
  //   alert("Vui lòng đăng nhập để bình luận!");
  //   return;
  if (commentText === "" && fileInput.files.length === 0) {
    alert("Vui lòng nhập nội dung bình luận hoặc chọn một hình ảnh!");
    return;
  }
  for (var i = 0; i < files.length; i++) {
    var file = files[i];
    var reader = new FileReader();

    reader.onload = function (e) {
      var imgElement = document.createElement("img");
      imgElement.src = e.target.result;
      imgElement.width = 100; // thiết lập chiều rộng cho hình ảnh
      newComment.appendChild(imgElement);
    };

    // đọc file như một URL
    reader.readAsDataURL(file);
  }

  commentList.appendChild(newComment);

  commentInput.value = "";
  fileInput.value = "";
}
