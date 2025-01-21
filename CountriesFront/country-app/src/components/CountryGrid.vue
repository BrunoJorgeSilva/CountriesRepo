<template>
  <div>
    <input v-model="filter" placeholder="Search by country name" />
    <table>
      <thead>
        <tr>
          <th>Name</th>
          <th>Capital</th>
          <th>Region</th>
          <th>SubRegion</th>
          <th>Population</th>
          <th>Latitude</th>
          <th>Longitude</th>
          <th>Borders</th>
          <th>Timezones</th>
          <th>Currency</th>
          <th>Languages</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="country in filteredCountries" :key="country.id">
          <td>{{ country.name }}</td>
          <td>{{ country.capital }}</td>
          <td>{{ country.region }}</td>
          <td>{{ country.subRegion }}</td>
          <td>{{ country.population }}</td>
          <td>{{ country.latitude }}</td>
          <td>{{ country.longitude }}</td>
          <td>{{ country.borders }}</td>
          <td>{{ country.timezones }}</td>
          <td>{{ country.currency }}</td>
          <td>{{ country.languages }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, computed, onMounted } from "vue";

interface Country {
  id: number;
  name: string;
  capital: string;
  region: string;
  subRegion: string;
  population: number;
  latitude: number;
  longitude: number;
  borders: string;
  timezones: string;
  currency: string;
  languages: string;
}

export default defineComponent({
  name: "CountryGrid",
  setup() {
    const filter = ref("");
    const countries = ref<Country[]>([]);

    const loadCountries = async () => {
      try {
        const response = await fetch(
          "https://localhost:44367/api/Countries/GetAllCountries"
        );
        const data = await response.json();
        if (data.isSuccess) {
          countries.value = data.value.map((country: Country) => ({
            id: country.id,
            name: country.name,
            capital: country.capital,
            region: country.region,
            subRegion: country.subRegion,
            population: country.population,
            latitude: country.latitude,
            longitude: country.longitude,
            borders: country.borders,
            timezones: country.timezones,
            currency: country.currency,
            languages: country.languages,
          }));
        } else {
          console.error("Error:", data.errorMessage);
        }
      } catch (error) {
        console.error("Error:", error);
      }
    };

    onMounted(() => {
      loadCountries();
    });

    const filteredCountries = computed(() => {
      return countries.value.filter((country) =>
        country.name.toLowerCase().includes(filter.value.toLowerCase())
      );
    });

    return {
      filter,
      countries,
      filteredCountries,
    };
  },
});
</script>

<style scoped>
filter {
  background-color: #9aa5a8;
  border-color: #1e667e;
}

input {
  margin-bottom: 10px;
}

table {
  width: 100%;
  border-collapse: collapse;
  background-color: #bdcbcd;
}

th,
td {
  padding: 10px;
  text-align: left;
}

tr:nth-child(even) {
  background-color: #feffff;
}
</style>
