const express = require("express");
const mongoose = require("mongoose");
const app = express();

const uri = 'mongodb+srv://Group4:ToiYeuMonNay@group4.okgrvdw.mongodb.net/?retryWrites=true&w=majority&appName=Group4';

async function connect () {
    try {
        await mongoose.connect(uri)
        console.log("Connected to MongoDB");
    } catch (error) {
        console.error(error);
    }
}

connect();

app.listen(8000, () => {
    console.log("Server started on port 8000");
})