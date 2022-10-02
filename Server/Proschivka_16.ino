
// ------------------ Начальные условия и исходные данные НАЧАЛО
// УПРАВЛЯЮЩИЕ ПАРАМЕТРЫ:
// 1.1)  Ид_строка_1    - Идентификатор строки управляющих параметров датчиков
// 1.2)  Вкл_темп_упр   - Включение/выключение измерений температуры
// 1.3)  Вкл_звук_упр   - Включение/выключение измерений звука
// 1.4)  Вкл_свет_упр   - Включение/выключение измерений света
// 1.5)  Вкл_вибр_упр   - Включение/выключение измерений вибрации
// 1.6)  Пер_тем_вл_упр - Период измерения температуры и влажности управляющий
// 1.7)  Пер_звук_упр   - Период измерения звука управляющий
// 1.8)  Пер_свет_упр   - Период измерения света управляющий
// 1.9)  Пер_вибр_упр   - Период измерения вибрации управляющий
// 1.10) Пер_всех_упр   - Период измерения всех датчиков управляющий
// 1.11) Строка_1       - Строка управляющих параметров датчиков

// Тестовая строка для подключения датчика температуры и влажности: 1;1;0;0;0;2;0;0;0;0;
// Тестовая строка для подключения датчика звука:                   1;0;1;0;0;0;2;0;0;0;
// Тестовая строка для подключения датчика света:                   1;0;0;1;0;0;0;2;0;0;
// Тестовая строка для подключения датчика вибрации:                1;0;0;0;1;0;0;0;2;0;
// Тестовая строка для подключения всех датчиков:                   1;1;1;1;1;0;0;0;0;2;

// Тестовая строка для подключения сервопривода:                    2;1;1;1;1;0;


// Ид_строка_1 = 1
// Вкл_темп_упр = {0;1}
// Вкл_звук_упр = {0;1}
// Вкл_свет_упр = {0;1}
// Вкл_вибр_упр = {0;1}

// Строка управляющих параметров датчиков:
//         1.1          1.2          1.3          1.4          1.5            1.6          1.7          1.8          1.9         1.10
// Ид_строки_1;Вкл_темп_упр;Вкл_звук_упр;Вкл_свет_упр;Вкл_вибр_упр;Пер_тем_вл_упр;Пер_звук_упр;Пер_свет_упр;Пер_вибр_упр;Пер_всех_упр;

// 2.1)  Ид_строка_2    - Идентификатор строки управляющих параметров сервопривода
// 2.2)  Вкл_серв_упр   - Включение/выключение работы сервопривода
// 2.3)  Нач_п_серв_упр - Начальное положение сервопривода управляющее
// 2.4)  Напр_вр_упр    - Направление вращения сервопривода от начального положения управляющее
// 2.5)  Уг_пов_упр     - Угол поворота сервопривода управляющий
// 2.6)  Скор_вр_упр    - Скорость вращения сервопривода управляющая
// 2.7)  Строка_2       - Строка управляющих параметров сервопривода

//       Ид_строка_2 = 2
//       Вкл_серв_упр = {0;1}, где
//                                0 - выключение сервопривода,
//                                1 - включение сервопривода,
//       Нач_п_серв_упр = {0;180}
//       Напр_вр_упр = {0;1}, где
//                               0 - направление вращения по часовой стрелке,
//                               1 - направление вращения против часовой стрелки
//       Уг_пов_упр = {0;180}
//       Скор_вр_упр = {0;250}

// Строка управляющих параметров сервопривода:
//         2.1          2.2            2.3         2.4        2.5         2.6
// Ид_строка_2;Вкл_серв_упр;Нач_п_серв_упр;Напр_вр_упр;Уг_пов_упр;Скор_вр_упр;

// ИЗМЕРИТЕЛЬНЫЕ ПАРАМЕТРЫ:
// 3.1) Ид_строка_3    - Идентификатор строки измерительных параметров датчиков
// 3.2) Темп_изм       - Температура измерительная
// 3.3) Влаж_изм       - Влажность измерительная
// 3.4) Звук_изм       - Громкость звука измерительная
// 3.5) Свет_изм       - Яркость света измерительная
// 3.6) Вибр_изм       - Сила вибрации измерительная
// 3.7) Строка_3       - Строка измерительных параметров датчиков

//      Ид_строка_3 = 3

// Строка измерительных параметров датчиков:
//         3.1      3.2      3.3      3.4      3.5      3.6
// Ид_строка_3;Темп_изм;Влаж_изм;Звук_изм;Свет_изм;Вибр_изм;

// 4.1) Ид_строка_4    - Идентификатор строки измерительных параметров сервопривода
// 4.2) Нач_п_серв_изм - Начальное положение сервопривода измерительное
// 4.3) Напр_вр_изм    - Направление вращения сервопривода от начального положения измерительное
// 4.4) Уг_пов_изм     - Угол поворота сервопривода измерительный
// 4.5) Строка_4       - Строка измерительных параметров сервопривода

//      Ид_строка_4 = 4

// Строка измерительных параметров сервопривода:
//         4.1            4.2         4.3        4.4         4.5
// Ид_строка_4;Нач_п_серв_изм;Напр_вр_изм;Уг_пов_упр;Скор_вр_упр;
 
// PIN D12 - подключение метеодатчика
// PIN A00 - подключение датчика звука
// PIN A03 - подключение датчика света
// PIN A05 - подключение датчика вибрации
// PIN D10 - подключение сервопривода

// ------------------ Начальные условия и исходные данные КОНЕЦ

// ------------------ Раздел подключения внешних библиотек и создания переменных НАЧАЛО

#include <DHT.h>
// #include <Servo.h>
#include <VarSpeedServo.h>

DHT dht(12, DHT22);
float  ID_Stroka_1;    // 1.1)  Ид_строка_1    - Идентификатор строки управляющих параметров датчиков
float  Vkl_temp_vl_upr;   // 1.2)  Вкл_темп_упр   - Включение/выключение измерений температуры
float  Vkl_zvuk_upr;   // 1.3)  Вкл_звук_упр   - Включение/выключение измерений звука
float  Vkl_svet_upr;   // 1.4)  Вкл_свет_упр   - Включение/выключение измерений света
float  Vkl_vibr_upr;   // 1.5)  Вкл_вибр_упр   - Включение/выключение измерений вибрации
float  Per_tem_vl_upr; // 1.6)  Пер_тем_вл_упр - Период измерения температуры и влажности управляющий
float  Per_zvuk_upr;   // 1.7)  Пер_звук_упр   - Период измерения звука управляющий
float  Per_svet_upr;   // 1.8)  Пер_свет_упр   - Период измерения света управляющий
float  Per_vibr_upr;   // 1.9)  Пер_вибр_упр   - Период измерения вибрации управляющий
float  Per_All_upr;    // 1.10) Пер_всех_упр   - Период измерения всех датчиков управляющий
String Stroka_1;       // 1.11) Строка_1       - Строка управляющих параметров датчиков

float  ID_Stroka_2;    // 2.1)  Ид_строка_2    - Идентификатор строки управляющих параметров сервопривода
float  Vkl_serv_upr;   // 2.2)  Вкл_серв_упр   - Включение/выключение работы сервопривода
float  Nach_p_serv_upr;// 2.3)  Нач_п_серв_упр - Начальное положение сервопривода управляющее
float  Napr_vr_upr;    // 2.4)  Напр_вр_упр    - Направление вращения сервопривода от начального положения управляющее
float  ug_pov_upr;     // 2.5)  Уг_пов_упр     - Угол поворота сервопривода управляющий
float  skor_vr_upr;    // 2.6)  Скор_вр_упр    - Скорость вращения сервопривода управляющая
String Stroka_2;       // 2.7)  Строка_2       - Строка управляющих параметров сервопривода

float  ID_Stroka_3;    // 3.1) Ид_строка_3    - Идентификатор строки измерительных параметров датчиков
float  Temp_izm;       // 3.2) Темп_изм       - Температура измерительная
float  Vl_izm;         // 3.3) Влаж_изм       - Влажность измерительная
float  Zvuk_izm;       // 3.4) Звук_изм       - Громкость звука измерительная
float  Svet_izm;       // 3.5) Свет_изм       - Яркость света измерительная
float  Vibr_izm;       // 3.6) Вибр_изм       - Сила вибрации измерительная
String Stroka_3;       // 3.7) Строка_3       - Строка измерительных параметров датчиков

float  ID_Stroka_4;    // 4.1) Ид_строка_4    - Идентификатор строки измерительных параметров сервопривода
float  Nach_p_serv_izm;// 4.2) Нач_п_серв_изм - Начальное положение сервопривода измерительное
float  Napr_vr_izm;    // 4.3) Напр_вр_изм    - Направление вращения сервопривода от начального положения измерительное
float  Ug_pov_izm;     // 4.4) Уг_пов_изм     - Угол поворота сервопривода измерительный
String Stroka_4;       // 4.5) Строка_4       - Строка измерительных параметров сервопривода

float ID_Stroka_5;        // 5.1) Ид_строка_5    - Идентификатор строки параметров сервопривода в автоматическом режиме
float  Vkl_serv_upr_avt;  // 5.2) Вкл_серв_упр_авт    - Включение сервопривода в автоматическом режиме
String Stroka_6;          // 5.3) Строка_6       - Строка управляющих параметров сервопривода в автоматическом режиме
String Stroka_7;          // 5.4) Строка_7       - Строка измерительных параметров сервопривода в автоматическом режиме

float ID_Stroka_6;          // 6.1) Ид_строка_6    - Идентификатор строки переводящей сервопривод в начальное положение
float  Vkl_serv_nach_pol;   // 6.2) Вкл_серв_нач_пол    - Включение сервопривода и его установка в начальное положение
String Stroka_8;            // 6.3) Строка_8       - Строка управляющих параметров сервопривода для его установки в начальное положение

int    Count_Byte;     // Количество поступивших байт в COM-порт
String Stroka_All;

int    Index_Poisk_Razdel; // Порядковый номер текущего символа, предшествующего разделителю, в текущей строке, состоящей из управляющих параметров
int    ii;                 // Порядковый номер атомарных значений из текущей строки, состоящей из управляющих параметров
String Atom_Str;           // Текущее атомарное значение строкового типа из текущей строки, состоящей из управляющих параметров
float  Atom_Float;         // Текущее атомарное значение вещественного типа из текущей строки, состоящей из управляющих параметров
float  Massiv_Atom[10];     // Массив атомарных значений из текущей строки, состоящей из управляющих параметров
int    i;                  // Счетчик для всех циклов типа "for"

VarSpeedServo ServoMotor;  // Объект управления сервоприводом

int Flag;                  // Управлящий параметр, отключающий и переключающий пораздельно работу датчиков и сервопривода
int Status_Napr_vr_upr;    // Управлящий параметр вычисления относительного направления и угла поворота
int Tek_pol_ser;           // Текущее положение сервопривода
int Tek_Nach_pol_ser;      // Текущее положение сервопривода

// ------------------ Раздел подключения внешних библиотек и создания переменных КОНЕЦ

void parse(int len, String string) //функция парсинга входной строки команды
{
  for (int i=0; i<len; i++)
           {
              Index_Poisk_Razdel = string.indexOf(';', 0);
              Atom_Str = string.substring(0, Index_Poisk_Razdel);
              Atom_Float = Atom_Str.toFloat();
              Massiv_Atom[i] = Atom_Float;
              string.remove(0, Index_Poisk_Razdel+1);
           }
}
void setup()
{ 
  Serial.begin(9600);
  pinMode(12, INPUT);
  pinMode(3, OUTPUT);
  pinMode(4, OUTPUT);
  pinMode(5, OUTPUT);
  pinMode(6, OUTPUT);
  pinMode(7, OUTPUT);
  dht.begin();
  ServoMotor.attach(10);
  
  ID_Stroka_1 = 0;
  Vkl_temp_vl_upr = 0;
  Vkl_zvuk_upr = 0;
  Vkl_svet_upr = 0;
  Vkl_vibr_upr = 0;
  Per_tem_vl_upr = 0;
  Per_zvuk_upr = 0;
  Per_svet_upr = 0;
  Per_vibr_upr = 0;
  Per_All_upr = 0;
  Stroka_1 = "";

  ID_Stroka_2 = 0;
  Vkl_serv_upr = 0;
  Nach_p_serv_upr = 0;
  Napr_vr_upr = 0;
  ug_pov_upr = 0;
  skor_vr_upr = 0;
  Stroka_2 = "";

  ID_Stroka_3 = 3;
  Temp_izm = 0;
  Vl_izm = 0;
  Zvuk_izm = 0;
  Svet_izm = 0;
  Vibr_izm = 0;
  Stroka_3 = "";

  ID_Stroka_4 = 4;
  Nach_p_serv_izm = 0;
  Napr_vr_izm = 0;
  Ug_pov_izm = 0;
  Stroka_4 = "";

  Count_Byte = 0;
  Stroka_All = "";

  Flag = 0;
  Status_Napr_vr_upr = 0;
  Tek_pol_ser = 0;
  
  ID_Stroka_5 = 0;
  Vkl_serv_upr_avt = 0;
  Stroka_6="";
  Stroka_7="";

  ID_Stroka_6=0;
  Vkl_serv_nach_pol=0;
  Stroka_8="";
}

void loop()
{ 
  Count_Byte = 0;
  if (Serial.available() > 11)
     {
       delay(100);
       Count_Byte = Serial.available();
       Stroka_All = "";
       delay(100);
       Stroka_All = Serial.readString();
     }
     
  if (Stroka_All[0] == '1' && Count_Byte >= 20)
     {
               Flag = 1;
               Stroka_1 = Stroka_All;
                    parse(10,Stroka_1);
                    ID_Stroka_1     = Massiv_Atom[0];
                    Vkl_temp_vl_upr = Massiv_Atom[1];
                    Vkl_zvuk_upr    = Massiv_Atom[2];
                    Vkl_svet_upr    = Massiv_Atom[3];
                    Vkl_vibr_upr    = Massiv_Atom[4];
                    Per_tem_vl_upr  = Massiv_Atom[5];
                    Per_zvuk_upr    = Massiv_Atom[6];
                    Per_svet_upr    = Massiv_Atom[7];
                    Per_vibr_upr    = Massiv_Atom[8];
                    Per_All_upr     = Massiv_Atom[9];
     }               
     else if (Stroka_All[0] == '2' && Count_Byte >= 12)
             {
                Flag = 2;
                Stroka_2 = Stroka_All;
                parse(6,Stroka_2);
                 ID_Stroka_2     = Massiv_Atom[0];
                 Vkl_serv_upr    = Massiv_Atom[1];
                 Nach_p_serv_upr = Massiv_Atom[2];
                 Napr_vr_upr     = Massiv_Atom[3];
                 ug_pov_upr      = Massiv_Atom[4];
                 skor_vr_upr     = Massiv_Atom[5];
              }
      else if (Stroka_All[0] == '3' && Count_Byte >= 12)
      {
        Flag = 3;
         Stroka_6 = Stroka_All;
                parse(2,Stroka_6);
                 ID_Stroka_5     = Massiv_Atom[0];
                 Vkl_serv_upr_avt    = Massiv_Atom[1];
      }
      else if (Stroka_All[0] == '4' && Count_Byte >= 12)
      {
        Flag = 4;
        Stroka_8 = Stroka_All;
        parse(2,Stroka_8);
                 ID_Stroka_6     = Massiv_Atom[0];
                 Vkl_serv_nach_pol    = Massiv_Atom[1];
      }
  if (Flag == 1)
     {
                    if(Vkl_svet_upr==1)
                          {
                               digitalWrite(3, HIGH);
                          }
                    else
                          {
                            digitalWrite(3, LOW);
                          }
                    if(Vkl_vibr_upr==1)
                          {
                             digitalWrite(6, HIGH);
                          }
                    else
                          {
                            digitalWrite(6, LOW);
                          }
                    if(Vkl_zvuk_upr==1)
                          {
                              digitalWrite(5, HIGH);
                          }
                    else
                          {
                            digitalWrite(5, LOW);
                          }
                    if(Vkl_temp_vl_upr==1)
                          {
                              digitalWrite(4, HIGH);
                          }
                    else
                          {
                            digitalWrite(4, LOW);
                          }
               if (Vkl_temp_vl_upr == 1 && Vkl_zvuk_upr == 0 && Vkl_svet_upr == 0 && Vkl_vibr_upr == 0)
                  {
                     Stroka_3 = "";
                     Temp_izm = dht.readTemperature();
                     Vl_izm   = dht.readHumidity();
                     delay (Per_tem_vl_upr*1000);
                     Stroka_3 = String(ID_Stroka_3, 0) + ';' + String(Temp_izm,1) + ';' + String(Vl_izm,1) + ';' + '0' + ';' + '0' + ';' + '0';
                     Stroka_3.replace(" ","");
                     //Serial.print("Stroka_31=");
                     Serial.println(Stroka_3);
                  }
                  else if (Vkl_temp_vl_upr == 0 && Vkl_zvuk_upr == 1 && Vkl_svet_upr == 0 && Vkl_vibr_upr == 0)
                          {
                             Stroka_3 = "";
                             Zvuk_izm = analogRead(0);
                             delay (Per_zvuk_upr*1000);
                             Stroka_3 = String(ID_Stroka_3, 0) + ';' + '0' + ';' + '0' + ';' + String(Zvuk_izm,0) + ';' + '0' + ';' + '0';
                             Stroka_3.replace(" ","");
                             //Serial.print("Stroka_32=");
                             Serial.println(Stroka_3);
                          }
                  else if (Vkl_temp_vl_upr == 0 && Vkl_zvuk_upr == 0 && Vkl_svet_upr == 1 && Vkl_vibr_upr == 0)
                          {
                             Stroka_3 = "";
                             Svet_izm = analogRead(3);
                             delay (Per_svet_upr*1000);
                             Stroka_3 = String(ID_Stroka_3, 0) + ';' + '0' + ';' + '0' + ';' + '0' + ';' + String(Svet_izm,0) + ';' + '0';
                             Stroka_3.replace(" ","");
                             //Serial.print("Stroka_33=");
                             Serial.println(Stroka_3);
                          }
                  else if (Vkl_temp_vl_upr == 0 && Vkl_zvuk_upr == 0 && Vkl_svet_upr == 0 && Vkl_vibr_upr == 1)
                          {
                            Stroka_3 = "";
                            Vibr_izm = analogRead(5);
                            delay (Per_vibr_upr*1000);
                            Stroka_3 = String(ID_Stroka_3, 0) + ';' + '0' + ';' + '0' + ';' + '0' + ';' + '0' + ';' + String(Vibr_izm,0);
                            Stroka_3.replace(" ","");
                            //Serial.print("Stroka_34=");
                            Serial.println(Stroka_3);
                          }
                  else if (Vkl_temp_vl_upr == 1 && Vkl_zvuk_upr == 1 && Vkl_svet_upr == 1 && Vkl_vibr_upr == 1)
                          {
                             Stroka_3 = "";
                             Temp_izm = dht.readTemperature();
                             Vl_izm   = dht.readHumidity();
                             Zvuk_izm = analogRead(0);
                             Svet_izm = analogRead(3);
                             Vibr_izm = analogRead(5);
                             delay (Per_All_upr*1000);
                             Stroka_3 = String(ID_Stroka_3, 0) + ';' + String(Temp_izm,1) + ';' + String(Vl_izm,1) + ';' + String(Zvuk_izm,0) + ';' + String(Svet_izm,0) + ';' + String(Vibr_izm,0) + ';';
                             Stroka_3.replace(" ","");
                             //Serial.print("Stroka_35=");
                             Serial.println(Stroka_3);
                          }
                          
     }
        else if (Flag == 2)                
                {
                  if (Vkl_serv_upr == 1)
                     {
                      digitalWrite(7, HIGH);
                        Stroka_4 = "";
                        if (Napr_vr_upr == 1)
                           {
                              Status_Napr_vr_upr = 1;
                           }
                           else if (Napr_vr_upr == 0)
                                   {
                                      Status_Napr_vr_upr = -1;
                                   }
                        if ((Status_Napr_vr_upr == -1 && ug_pov_upr <= Nach_p_serv_upr && ug_pov_upr > 0 && Nach_p_serv_upr != 0) ||
                            (Status_Napr_vr_upr == 1  && ug_pov_upr <= (180 - Nach_p_serv_upr) && ug_pov_upr > 0 && Nach_p_serv_upr != 180))
                           {
                              if (Nach_p_serv_upr != Tek_Nach_pol_ser)
                                 {
                                    ServoMotor.write(Nach_p_serv_upr, skor_vr_upr, true);
                                    Nach_p_serv_izm = ServoMotor.read();
                                    delay(1000);
                                 }
                              ServoMotor.write(Nach_p_serv_upr + ug_pov_upr*Status_Napr_vr_upr, skor_vr_upr, true);
                              Tek_pol_ser = ServoMotor.read();
                              if (Tek_pol_ser < Nach_p_serv_upr)
                                 {
                                    Napr_vr_izm = 0;
                                 }
                                 else if (Tek_pol_ser > Nach_p_serv_upr)
                                         {
                                            Napr_vr_izm = 1;
                                         }
                              Ug_pov_izm = abs(Tek_pol_ser - Nach_p_serv_upr);
                              Stroka_4 = String(ID_Stroka_4, 0) + ';' + String(Nach_p_serv_izm,0) + ';' + String(Napr_vr_izm,0) + ';' + String(Ug_pov_izm,0)+';'+ String(skor_vr_upr,0)+';';
                              Stroka_4.replace(" ","");
                              //Serial.print("Stroka_4=");
                              Serial.println(Stroka_4);
                              Tek_Nach_pol_ser = Nach_p_serv_upr;
                           }
                              else if (Status_Napr_vr_upr == -1 && ug_pov_upr > (180 - Nach_p_serv_upr) || Nach_p_serv_upr == 0)
                                 {
                                    Serial.println ("При заданном направлении вращения по часовой стрелке:");
                                    Serial.println ("задан угол поворота больше начального положения или");
                                    Serial.println ("задано начальное положение, равное граничному положению - нулю градусов.");
                                 }
                                 else if (Status_Napr_vr_upr == 1 && ug_pov_upr > Nach_p_serv_upr || Nach_p_serv_upr == 180)
                                         {
                                           Serial.println ("При заданном направлении вращения против часовой стрелки:");
                                           Serial.println ("задан угол поворота больше 180 градусов или");
                                           Serial.println ("задано начальное положение, равное граничному положению - 180 градусам.");
                                         }
                                 else if (ug_pov_upr == 0)
                                         {
                                            Serial.println ("Задан угол поворота, равный нулю градусов");
                                         }

                            
                           }
                           else
                              digitalWrite(7, LOW);
                        Flag = 0;    
                     }
          else if(Flag==3)
          {
            if (Vkl_serv_upr_avt == 1)
                     {
                      digitalWrite(7, HIGH);
                      Stroka_7="";
                      ServoMotor.write(0, 250, true);
                      delay(1000);
                      ServoMotor.write(140, 100, true);
                      Stroka_7 = "5;0;1;140;100;";
                      Serial.println(Stroka_7);
                      delay(1000);
                      ServoMotor.write(30, 50, true);
                      Stroka_7 = "5;140;0;110;50;";
                      Serial.println(Stroka_7);
                      delay(1000);
                      ServoMotor.write(180, 250, true);
                      Stroka_7 = "5;30;1;150;250;";
                      Serial.println(Stroka_7);
                      delay(1000);
                      ServoMotor.write(0, 150, true);
                      Stroka_7 = "5;180;0;180;150;";
                      Serial.println(Stroka_7);
                      delay(1000);
                      ServoMotor.write(60, 10, true);
                      Stroka_7 = "5;0;1;60;10;";
                      Serial.println(Stroka_7);
                     }
                else
                  digitalWrite(7, LOW);
            Flag = 0; 
          }           
       else if(Flag==4)
       {
        if(Vkl_serv_nach_pol==1)
            ServoMotor.write(0, 250, true);
            Flag = 0;
        }             
}



  

  
