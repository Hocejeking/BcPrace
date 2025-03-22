import torch
import pandas as pd
from transformers import AutoModelForCausalLM, AutoTokenizer, pipeline

# 1. Načtení modelu LLaMA 70B
model_name = "meta-llama/Llama-2-70b-chat-hf"
tokenizer = AutoTokenizer.from_pretrained(model_name)
model = AutoModelForCausalLM.from_pretrained(
    model_name,
    torch_dtype=torch.float16,
    device_map="auto"
)
generator = pipeline("text-generation", model=model, tokenizer=tokenizer, device=0)

# 2. Načtení CSV souboru
def process_csv(input_csv, output_csv):
    df = pd.read_csv(input_csv)
    if "text" not in df.columns:
        raise ValueError("Sloupec 'text' nebyl nalezen v datasetu.")
    
    new_texts = []
    for text in df["text"]:
        prompt = f"Přepiš následující e-mail do profesionální podoby:\n{text}\n"
        output = generator(prompt, max_length=512, do_sample=True, temperature=0.7)
        new_texts.append(output[0]["generated_text"].replace(prompt, ""))
    
    df["text_rewritten"] = new_texts
    df.to_csv(output_csv, index=False)
    print(f"Výstup uložen do {output_csv}")

# 3. Spuštění zpracování souboru
input_csv = "emails.csv"  # Nahraď vlastním názvem
output_csv = "emails_rewritten.csv"
process_csv(input_csv, output_csv)