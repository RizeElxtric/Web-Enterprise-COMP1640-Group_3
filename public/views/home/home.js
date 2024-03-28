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
  var commentText = commentInput.value.trim(); // Loại bỏ các khoảng trắng ở đầu và cuối

  var files = fileInput.files;

  // Kiểm tra nếu chỉ có khoảng trắng hoặc không có nội dung và không có file
  if (commentText === "" && files.length === 0) {
    alert("Vui lòng nhập nội dung bình luận hoặc chọn một hình ảnh!");
    return;
  }

  var newComment = document.createElement("div");
  newComment.classList.add("comment");

  var userIdText = document.createElement("p");
  userIdText.textContent = "User ID: " + loggedInUser.id;
  newComment.appendChild(userIdText);

  var userNameText = document.createElement("p");
  userNameText.textContent = "User Name: " + loggedInUser.name;
  newComment.appendChild(userNameText);

  var timestamp = document.createElement("p");
  timestamp.textContent = "Posted at: " + new Date().toLocaleString();
  newComment.appendChild(timestamp);

  var commentContent = document.createElement("textarea");
  commentContent.textContent = commentText;
  commentContent.classList.add("commentContent");
  commentContent.disabled = true; // Chỉnh sửa không được phép mặc định
  newComment.appendChild(commentContent);

  var deleteButton = document.createElement("button");
  deleteButton.textContent = "Xóa";
  deleteButton.onclick = function () {
    this.parentNode.remove(); // Xóa bình luận khỏi DOM
  };
  newComment.appendChild(deleteButton);

  var editButton = document.createElement("button");
  editButton.textContent = "Sửa";
  editButton.onclick = function () {
    // Thực hiện logic sửa bình luận ở đây
    // Các thay đổi cụ thể có thể được thực hiện ở đây
  };
  newComment.appendChild(editButton);

  // Nếu có file được chọn, hiển thị ảnh
  for (var j = 0; j < files.length; j++) {
    var file = files[j];
    var reader = new FileReader();

    reader.onload = function (e) {
      var imgElement = document.createElement("img");
      imgElement.src = e.target.result;
      imgElement.height = 100;
      imgElement.width = 100;
      newComment.appendChild(imgElement);
    };

    reader.readAsDataURL(file);
  }

  commentList.appendChild(newComment);

  // Xóa nội dung trong textarea
  commentInput.value = "";
  fileInput.value = "";
}
