import ollama
import pandas as pd

model_name = "emailSpammer:latest"
def rewrite_email(email_text):
    print(f"I will attempt to rewrite this message: {email_text}")
    response = ollama.chat(
        model=model_name,
        messages=[
            {"role": "user", "content": email_text}
        ]
    )

    return response["message"]["content"]

# Load CSV
input_csv = "enron_random_sample.csv"
output_csv = "emails_rewritten.csv"
df = pd.read_csv(input_csv, delimiter=";")

if 'Message' not in df.columns:
    raise ValueError("CSV file does not contain a 'Message' column.")

rewritten_texts = []

for text in df['Message']:
    if pd.notna(text):
        rewritten_texts.append(rewrite_email(text))
    else:
        rewritten_texts.append("")

df['rewritten_text'] = rewritten_texts

df.to_csv(output_csv, index=False, sep=";")

print(f"Rewritten emails saved to {output_csv}")