import axios from "axios";
import { useEffect, useState } from "react";

function App() {
  const [file, setFile] = useState<File | null>(null);
  const [text, setText] = useState("");
  useEffect(() => {
    console.log("file", file);

    axios.get("http://localhost:5000/").then((res) => {
      console.log(res.data);
    });
    if (file) {
      const formData = new FormData();
      formData.append("imageFile", file);
      formData.append("imageText", file.name);

      (async () => {
        try {
          console.time("API Call");
          const response = await axios.post(
            "http://localhost:5000/recognize",
            formData
          );
          console.timeEnd("API Call");
          console.log(response.data);
          setText(response.data.text);
        } catch (error) {
          console.log(error);
        }
      })();
    }
  }, [file]);

  return (
    <>
      <div className="text-bold text-2xl">
        <h2>Hello - Enumbers - OCR Text reader</h2>
        <input
          onChange={(e) => {
            e.target.files && setFile(e.target.files[0]);
          }}
          type="file"
          accept="image/*"
        />
        <p>{text}</p>
      </div>
    </>
  );
}

export default App;
