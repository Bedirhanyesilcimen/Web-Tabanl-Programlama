using System.Text;
using Newtonsoft.Json;

namespace WorkFlowApp.Services
{
    public class YapayZekaService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public YapayZekaService()
        {
            // API Key'in buraya gelecek
            _apiKey = "AIzaSyBZKHdKxctqwnI8po7vtslBSP7X_rTS1Eg"; 
            _httpClient = new HttpClient();
        }

        public async Task<string> FiyatAnaliziYap(string urunBilgileri, string piyasaDurumu)
        {
            // SON DENEME: "gemini-1.5-flash-latest" (En güncel sürüm etiketi)
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent?key={_apiKey}";

            var requestBody = new
            {
                contents = new[] { new { parts = new[] { new { text = $"Şu verilere göre fiyat önerisi yap: {urunBilgileri}. Durum: {piyasaDurumu}" } } } }
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(url, jsonContent);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseString);
                    return data?.candidates[0]?.content?.parts[0]?.text ?? "Cevap alınamadı.";
                }
                else
                {
                    // HATA OLSA BİLE SİSTEMİ DURDURMA, SAHTE CEVAP DÖN
                    // Bu sayede projene devam edebilirsin, AI ile sonra ilgileniriz.
                    return "⚠️ Yapay Zeka Bağlantı Sorunu: API Modeli şu an yanıt vermiyor.\n\n" +
                           "MANUEL ÖNERİ SİSTEMİ DEVREDE:\n" +
                           "- Maliyetler arttıysa %10-15 arası zam güvenli aralıktır.\n" +
                           "- Stok durumunuzu kontrol ederek kampanya yapabilirsiniz.";
                }
            }
            catch
            {
                // İNTERNET YOKSA BİLE PROJE ÇÖKMESİN
                return "⚠️ Bağlantı Hatası. Lütfen internetinizi kontrol edin.";
            }
        }
    }
}