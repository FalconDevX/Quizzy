# Quizzy

<img src="NewLoginGui/Resources/Logo.png" alt="Quizzy Logo" width="150">

**Quizzy** to aplikacja desktopowa napisana w języku C# przy użyciu **Windows Forms**, która umożliwia użytkownikom naukę poprzez quizy. Aplikacja obsługuje rejestrację i logowanie użytkowników, a także oferuje możliwość rozwiązywania quizów z różnych tematów. **Quizzy** to świetne narzędzie do szybkiej nauki i sprawdzania wiedzy.

## Funkcje

- **Rejestracja użytkownika**:
  - Użytkownicy mogą założyć konto, podając adres e-mail oraz hasło.
  - System weryfikuje poprawność adresu e-mail, a także sprawdza zgodność hasła i jego powtórzenia.
  - Hasła są tymczasowo przechowywane w postaci jawnej (w wersji testowej) – docelowo będą szyfrowane.

- **Logowanie użytkownika**:
  - Użytkownicy mogą logować się za pomocą wcześniej zarejestrowanego adresu e-mail i hasła.
  - Weryfikacja loginu odbywa się na podstawie zapisanych danych w bazie SQL.

- **Quizy**:
  - Użytkownicy mogą wybierać quizy, odpowiadać na pytania i śledzić swoje wyniki.
  - Aplikacja obsługuje pytania z kilkoma odpowiedziami, a także pytania z obrazkami (jeszcze do rozwinięcia).

- **Placeholdery**:
  - Pola tekstowe dla e-maila i hasła w formularzach logowania i rejestracji mają dynamiczne placeholdery, które znikają po rozpoczęciu wpisywania danych.

- **Walidacja danych**:
  - Aplikacja sprawdza poprawność wpisanego adresu e-mail.
  - Sprawdzenie, czy hasło i powtórzone hasło są zgodne, podczas rejestracji.

- **Pokazywanie/ukrywanie hasła**:
  - Użytkownicy mogą zdecydować, czy chcą wyświetlić wpisywane hasło, używając odpowiedniego checkboxa.

## Wymagania

- **.NET Framework 4.7.2** lub nowszy
- **SQL Server** (lokalny lub w chmurze, np. Azure SQL Database)
- **Visual Studio 2019/2022** (lub nowsze)

## Instalacja

### Krok 1: Sklonowanie repozytorium

Sklonuj repozytorium na swój lokalny komputer:

```bash
git clone https://github.com/username/quizzy.git
