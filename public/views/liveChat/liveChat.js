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

var chatBox = document.getElementById("chat_box");

function send() {
  var msg = document.getElementById("chat_input").value;
  if (msg != "") {
    chatBox.innerHTML += "<p>" + msg + "</p>";
    document.getElementById("chat_input").value = "";
    chatBox.scrollTop = chatBox.scrollHeight;
  }
}

function upload() {
  var file = document.getElementById("fileUpload").files[0];
  if (file) {
    if (file.type.match("image.*")) {
      var reader = new FileReader();
      reader.onloadend = function () {
        chatBox.innerHTML +=
          '<p><img src="' +
          reader.result +
          '" style="width:100px;height:100px;" alt="Uploaded image"></p>';
        chatBox.scrollTop = chatBox.scrollHeight;
      };
      reader.readAsDataURL(file);
    } else {
      chatBox.innerHTML += "<p>File uploaded: " + file.name + "</p>";
      chatBox.scrollTop = chatBox.scrollHeight;
    }
  }
}
