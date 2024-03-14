const express = require("express");
const mongoose = require("mongoose");
const path = require("path"); // Ensure that you import the 'path' module
const app = express();

const uri =
  "mongodb+srv://Group4:ToiYeuMonNay@group4.okgrvdw.mongodb.net/?retryWrites=true&w=majority&appName=Group4";

async function connect() {
  try {
    await mongoose.connect(uri);
    console.log("Connected to MongoDB");
  } catch (error) {
    console.error(error);
  }
}

connect();

// Middleware for serving static files
app.use(express.static(path.join(__dirname, "src", "views")));

// Route for the home page
app.get("/", (req, res) => {
  res.sendFile(path.join(__dirname, "src", "views", "home", "home.html"));
});
app.get("/dashboard", (req, res) => {
  res.sendFile(
    path.join(__dirname, "src", "views", "dashboard", "dashboard.html")
  );
});
app.get("/livechat", (req, res) => {
  res.sendFile(
    path.join(__dirname, "src", "views", "livechat", "livechat.html")
  );
});
app.get("/contact", (req, res) => {
  res.sendFile(path.join(__dirname, "src", "views", "contact", "contact.html"));
});
app.get("/login", (req, res) => {
  res.sendFile(path.join(__dirname, "src", "views", "login", "login.html"));
});

app.listen(8000, () => {
  console.log("Server started on port 8000");
});
