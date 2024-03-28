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

  // var commentContentText = document.createElement("p");
  // commentContentText.textContent = commentText;
  // newComment.appendChild(commentContentText);

  var timestamp = document.createElement("p");
  timestamp.textContent = "Posted at: " + new Date().toLocaleString();
  newComment.appendChild(timestamp);

  var commentContent = document.createElement("textarea");
  commentContent.textContent = commentText;
  commentContent.classList.add("commentContent");
  commentContent.disabled = true; // Chỉnh sửa không được phép mặc định

  newComment.appendChild(commentContent);

  // Thêm nút xóa và sửa vào bình luận
  var deleteButton = document.createElement("button");
  deleteButton.textContent = "Xóa";
  deleteButton.onclick = function () {
    // Xử lý xóa bình luận ở đây
    this.parentNode.remove(); // Xóa bình luận khỏi DOM
  };

  var isEditing = false; // Biến để theo dõi trạng thái sửa đổi

  var editButton = document.createElement("button");
  editButton.textContent = "Sửa";
  editButton.onclick = function () {
    if (!isEditing) {
      // Kiểm tra xem người dùng đã bắt đầu sửa chưa
      isEditing = false; // Đặt biến isEditing thành true khi người dùng bắt đầu sửa

      var commentContent = this.parentNode.querySelector(".commentContent");
      var existingText = commentContent.textContent;

      // Ẩn nút "Xóa"
      var deleteButton = this.parentNode.querySelector("button:first-of-type");
      deleteButton.style.display = "none";

      // Tạo một biến tạm thời để lưu trữ nội dung ban đầu của bình luận
      var tempText = existingText;

      var editBox = document.createElement("textarea");
      editBox.value = existingText;

      // Ẩn nội dung bình luận và hiển thị hộp văn bản chỉnh sửa
      commentContent.style.display = "none";
      this.parentNode.appendChild(editBox);

      // Tạo nút "Lưu" để lưu thay đổi
      var saveButton = document.createElement("button");
      saveButton.textContent = "Lưu";
      saveButton.onclick = function () {
        var newText = editBox.value;

        // Cập nhật nội dung của bình luận từ biến tạm thời
        tempText = newText;

        // Cập nhật nội dung của bình luận và ẩn hộp văn bản chỉnh sửa
        commentContent.textContent = newText;
        commentContent.style.display = "block";
        editBox.remove();
        saveButton.remove();

        // Hiển thị lại nút "Sửa"
        editButton.style.display = "inline-block";

        // Hiển thị lại nút "Xóa"
        deleteButton.style.display = "inline-block";

        // Đặt lại biến isEditing thành false
        isEditing = false;
      };
      this.parentNode.appendChild(saveButton);

      // Ẩn nút "Sửa" khi người dùng đang chỉnh sửa
      this.style.display = "none";
    }
  };
  newComment.appendChild(deleteButton);
  newComment.appendChild(editButton);

  commentList.appendChild(newComment);

  // Xóa nội dung trong textarea
  // commentInput.value = "";
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
// document.addEventListener("DOMContentLoaded", function () {
//   var courses = document.querySelectorAll(".course");

//   courses.forEach(function (course) {
//     course.addEventListener("click", function (e) {
//       if (!event.target.matches("button")) {
//         // chỉ thêm hiệu ứng khi người dùng nhấp vào khóa học, không phải button
//         this.classList.toggle("active");

//         var commentSection = this.querySelector(".commentSection");
//         if (this.classList.contains("active")) {
//           commentSection.style.maxHeight = "none";
//           commentSection.style.overflow = "visible";
//         } else {
//           commentSection.style.maxHeight = "400px";
//           commentSection.style.overflow = "auto";
//         }
//       }
//     });
//   });
// });
