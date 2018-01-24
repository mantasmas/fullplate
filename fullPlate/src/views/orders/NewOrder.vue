<template>
  <div class="orders">
    <div>
      <h2> Soups </h2>
      <div v-for="dish in oneLunchData.dishes" v-if="dish.dishType === dishTypes.SOUP">
        <md-radio  v-model="soupId" name="my-test-group1" :md-value="dish.id">
          {{dish.name}}
          {{dish.isVegetarian ? ' Vegetarian dish' : ''}}
        </md-radio>
      </div>
    </div>

    <div>
      <h2> Main dishes </h2>
      <div v-for="dish in oneLunchData.dishes" v-if="dish.dishType === dishTypes.MAIN">
        <md-radio  v-model="mainDishId" :md-value="dish.id">
          {{dish.name}}
          {{dish.isVegetarian ? ' Vegetarian dish' : ''}}
        </md-radio>
      </div>
    </div>
    <md-button class="md-raised wide-button" @click="onSubmit">Create Lunch</md-button>
    <md-button class="md-raised wide-button" @click="onCancel">Cancel Creation</md-button>
  </div>
</template>

<script>
  import {mapGetters} from 'vuex';
  import dishTypes from '../../utils/enums/dishTypes';

  export default {
    name: 'NewOrder',
    data () {
      return {
        soupId: null,
        mainDishId: null,
        dishTypes
      };
    },
    methods: {
      onSubmit () {
        this.$store.dispatch('saveOrder',
          {lunchId: this.$route.params.lunchId, soupId: this.soupId, mainDishId: this.mainDishId}).then((success) => {
            if (success) {
              this.$router.push('/available-lunches');
            }
          });
      },
      onCancel () {
        this.$router.push('/available-lunches');
      }
    },
    created () {
      this.$store.dispatch('getOneLunch', this.$route.params.lunchId);
    },
    components: {

    },
    computed: {
      ...mapGetters(['oneLunchData'])
    }
  };
</script>

<style>
  #dishType {
    width: 100px
  }
</style>
