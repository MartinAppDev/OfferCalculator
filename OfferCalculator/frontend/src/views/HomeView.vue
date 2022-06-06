<template>
  <div>
    <v-form ref="form" v-model="valid" lazy-validation>
      <v-card
        class="mx-auto mt-8"
        width="600px"
      >
      <v-card-title
          class="text-h5" v-if="hasResults"
        >
        <v-btn icon @click="hasResults=false">
          <v-icon>mdi-arrow-left</v-icon>
        </v-btn>
        Offer Calculator Results
      </v-card-title>
      <v-card-title class="text-h5" v-else>
          Offer Calculator
      </v-card-title>
      <v-card-text v-if="hasResults">
        <v-simple-table>
          <template v-slot:default>
            <tbody>
              <tr>
                <td>Total distance based costs</td>
                <td>{{ results.totalDistanceBasedConsts }}</td>
              </tr>
              <tr>
                <td>Total time based costs</td>
                <td>{{ results.totalTimeBasedCosts }}</td>
              </tr>
              <tr>
                <td>Income</td>
                <td>{{ results.income }}</td>
              </tr>
              <tr>
                <td>Distance Based Profitability</td>
                <td>{{ results.distanceBasedProfitability }}</td>
              </tr>
              <tr>
                <td>Time Based Profitability</td>
                <td>{{ results.timeBasedProfitability }}</td>
              </tr>
            </tbody>
          </template>
        </v-simple-table>
      </v-card-text>
      <v-card-text v-else>
        <v-col cols="12">
          <v-text-field
              v-model="fields.pricePerKilometre"
              :rules="requiredRule"
              label="Price per kilometre"
              required
            ></v-text-field>
        </v-col>
        <v-col cols="12">
          <v-text-field
              v-model="fields.pricePerHour"
              :rules="requiredRule"
              label="Price per hour"
              required
            ></v-text-field>
        </v-col>
        <v-col cols="12">
            <v-text-field
              v-model="fields.kilometers"
              :rules="requiredRule"
              label="Kilometers"
              required
            ></v-text-field>
        </v-col>
        <v-col cols="12">
            <v-text-field
              v-model="fields.hours"
              :rules="requiredRule"
              label="Hours"
              required
            ></v-text-field>
        </v-col>
        <v-col cols="12">
            <v-text-field
              v-model="fields.income"
              :rules="requiredRule"
              label="Income"
              required
            ></v-text-field>
        </v-col>
      </v-card-text>
      <v-card-actions v-if="!hasResults">
          <v-btn
            :disabled="!valid"
            color="success"
            class="mr-4"
            :loading="loading"
            @click="submit"
          >
            Submit
          </v-btn>
      </v-card-actions>
    </v-card>
  </v-form>
  </div>
</template>

<script>
export default {
  name: 'HomeView',
  data() {
    return {
      fields: {
        pricePerKilometre: 0,
        pricePerHour: 0,
        kilometers: 0,
        hours: 0,
        income: 0,
      },
      results: {
        totalDistanceBasedConsts: 0,
        totalTimeBasedCosts: 0,
        income: 0,
        distanceBasedProfitability: 0,
        timeBasedProfitability: 0,
      },
      requiredRule: [
        (v) => /^\s*[-+]?((\d+(\.\d+)?)|(\d+\.)|(\.\d+))(e[-+]?\d+)?\s*$/.test(v) || 'This field is required. Use dot as a decimal separator',
      ],
      valid: true,
      hasResults: false,
      loading: false,
    };
  },
  methods: {
    async submit() {
      if (!this.$refs.form.validate()) {
        return;
      }

      this.loading = true;

      const response = await fetch('/api/calculation', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json;charset=utf-8',
        },
        body: JSON.stringify(this.fields),
      });

      this.results = await response.json();
      this.hasResults = true;
      this.loading = false;
    },
  },
};
</script>
