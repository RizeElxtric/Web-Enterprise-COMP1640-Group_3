document.addEventListener("DOMContentLoaded", function () {
  const addUserButton = document.getElementById("add-user-button");
  const modal = document.getElementById("add-user-modal");
  const closeButton = modal.querySelector(".close");
  const form = document.getElementById("add-user-form");
  const userList = document.getElementById("user-list");

  addUserButton.addEventListener("click", function () {
    modal.style.display = "block";
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
    const name = document.getElementById("user-name").value;
    const email = document.getElementById("user-email").value;
    const role = document.getElementById("user-role").value;
    const userItem = createUserItem(name, email, role);
    userList.appendChild(userItem);
    modal.style.display = "none";
    form.reset();
  });

  function createUserItem(name, email, role) {
    const userItem = document.createElement("div");
    userItem.classList.add("user");
    userItem.innerHTML = `
        <h2>${name}</h2>
        <p>Email: ${email}</p>
        <p>Role: ${role}</p>
      `;
    return userItem;
  }
  const users = [
    { name: "John Doe", email: "john@example.com", role: "admin" },
    { name: "Jane Doe", email: "jane@example.com", role: "user" },
    { name: "Alice Smith", email: "alice@example.com", role: "user" },
    { name: "Bob Johnson", email: "bob@example.com", role: "user" },
  ];

  // Displaying users
  users.forEach((user) => {
    const userItem = createUserItem(user.name, user.email, user.role);
    userList.appendChild(userItem);
  });
});
