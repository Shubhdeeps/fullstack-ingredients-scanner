import axios from "axios";
import { useEffect, useState } from "react";

function App() {
  const [file, setFile] = useState<File | null>(null);

  useEffect(() => {
    console.log("file", file);

    axios.get("http://localhost:5056/").then((res) => {
      console.log(res.data);
    });
    if (file) {
      const formData = new FormData();
      formData.append("imageFile", file);
      formData.append("imageText", file.name);

      (async () => {
        try {
          const response = await axios.post(
            "http://localhost:5056/recognize",
            formData
          );
          console.log(response.data);
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
      </div>
    </>
  );
}

export default App;
