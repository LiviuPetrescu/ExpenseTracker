<template>
  <div class="mb-2 grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-1">
    <!-- Category Filter -->
    <select
      v-model="localFilters.category"
      class="w-full text-sm border border-border rounded p-2 focus:outline-none focus:ring-2 focus:ring-heading"
    >
      <option value="">All Categories</option>
      <option v-for="cat in categories" :key="cat" :value="cat">{{ cat }}</option>
    </select>

    <!-- Start Date -->
    <FlatPickr
      v-model="localFilters.startDate"
      placeholder="Start Date"
      class="w-full text-sm border border-border rounded p-2 focus:outline-none focus:ring-2 focus:ring-heading"
    />

    <!-- End Date -->
    <FlatPickr
      v-model="localFilters.endDate"
      placeholder="End Date"
      class="w-full text-sm border border-border rounded p-2 focus:outline-none focus:ring-2 focus:ring-heading"
    />

    <!-- Apply Button -->
    <button
      @click="$emit('apply')"
      class="w-full bg-logo-blue text-white px-4 py-2 rounded hover:bg-logo-blue-dark transition focus:outline-none focus:ring-2 focus:ring-logo-blue"
    >
      Apply Filters
    </button>
  </div>
</template>

<script setup>
import { reactive, watch } from 'vue'
import FlatPickr from 'vue-flatpickr-component'
import 'flatpickr/dist/flatpickr.css'

const props = defineProps({
  modelValue: {
    type: Object,
    required: true,
  },
  categories: {
    type: Array,
    default: () => [],
  },
})

const emit = defineEmits(['update:modelValue', 'apply'])

const localFilters = reactive({ ...props.modelValue })

watch(
  () => localFilters,
  () => {
    emit('update:modelValue', { ...localFilters })
  },
  { deep: true },
)
</script>
