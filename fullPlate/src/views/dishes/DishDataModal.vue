<template>
  <md-dialog md-open-from="#btn-new-dish" md-close-to="#btn-new-dish" ref="new-dish-dialog">
    <md-dialog-title v-if="editMode">Edit Dish</md-dialog-title>
    <md-dialog-title v-else>Create New Dish</md-dialog-title>
    <form v-on:submit.prevent="submitData">
      <md-dialog-content>
        <md-input-container>
          <label>Dish Name</label>
          <md-input
              v-model="editableDish.name"
              md-clearable="true"
              :required="true"
          />
        </md-input-container>

        <md-input-container :class="{'md-input-invalid': errors.has('decimal')}">
          <label>Dish Price</label>
          <md-input
              v-model="editableDish.price"
              md-clearable="true"
              :required="true"
              v-validate
              data-vv-name="decimal"
              data-vv-rules="required|decimal"
          />
          <span class="md-error">{{errors.first('decimal')}}</span>

        </md-input-container>

        <div>
          <md-radio v-model="editableDish.dishType" md-value="0">Soup</md-radio>
          <md-radio v-model="editableDish.dishType" md-value="1">Main</md-radio>
        </div>

        <md-checkbox v-model="editableDish.isVegetarian" class="md-primary">Is Vegetarian</md-checkbox>
      </md-dialog-content>

      <md-dialog-actions>
        <md-button @click="closeDialog">Cancel</md-button>
        <md-button type="submit">Add</md-button>
      </md-dialog-actions>
    </form>
  </md-dialog>
</template>

<script>
  export default {
    name: 'dish-data-modal',
    props: ['editMode', 'dishData'],
    data () {
      return {
        editableDish: this.dishData
      };
    },
    methods: {
      closeDialog () {
        this.$refs['new-dish-dialog'].close();
      },
      submitData () {
        this.$refs['new-dish-dialog'].close();

        const dishData = {
          restaurantId: this.$route.params.id,
          dishData: this.editableDish
        };

        if (!this.editMode) {
          this.$store.dispatch('addNewDish', dishData);
        } else {
          this.$store.dispatch('updateDish', dishData);
        }
      }
    },
    watch: {
      dishData: function () {
        this.editableDish = this.dishData;
      }
    }
  };
</script>