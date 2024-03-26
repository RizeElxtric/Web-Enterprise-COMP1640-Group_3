const express = require("express");
const mongoose = require("mongoose");
const path = require("path"); // Ensure that you import the 'path' module
const app = express();

const uri =
  "mongodb+srv://Group4:ToiYeuMonNay@group4.okgrvdw.mongodb.net/?retryWrites=true&w=majority&appName=Group4";

const Contribution = mongoose.model(
  "Contribution",
  new mongoose.Schema({
    title: String,
    description: String,
  }),
  "contributions"
);

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
app.use(express.static(path.join(__dirname, "public", "views")));
app.use(express.json());

// Route for the home page
app.get("/", (req, res) => {
  res.sendFile(path.join(__dirname, "public", "views", "home", "home.html"));
});
app.get("/dashboard", (req, res) => {
  res.sendFile(
    path.join(__dirname, "public", "views", "dashboard", "dashboard.html")
  );
});
app.get("/livechat", (req, res) => {
  res.sendFile(
    path.join(__dirname, "public", "views", "livechat", "livechat.html")
  );
});
app.get("/contact", (req, res) => {
  res.sendFile(
    path.join(__dirname, "public", "views", "contact", "contact.html")
  );
});
app.get("/login", (req, res) => {
  res.sendFile(path.join(__dirname, "public", "views", "login", "login.html"));
});
app.get("/contributions", async (req, res) => {
  try {
    const contributions = await Contribution.find();
    res.status(200).json(contributions);
  } catch (err) {
    console.error(err);
    res.status(500).send("Server error");
  }
});
app.post("/contributions", async (req, res) => {
  try {
    const contribution = new Contribution({
      title: req.body.title || "",
      description: req.body.description || "",
    });

    await contribution.save();
    res.status(201).send(contribution);
  } catch (err) {
    console.error(err);
    res.status(500).send("Server error");
  }
});
const PORT = process.env.PORT || 8000;
app.listen(PORT, () => {
  console.log(`Server started on port ${PORT}`);
});
