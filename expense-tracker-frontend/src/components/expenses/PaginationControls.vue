<template>
  <div class="flex flex-wrap justify-center items-center gap-2 mt-4 text-sm">
    <button
      :disabled="currentPage <= 1"
      @click="$emit('change-page', currentPage - 1)"
      class="px-4 py-2 bg-logo-blue text-white rounded disabled:opacity-50 hover:bg-logo-blue-dark transition"
    >
      Previous
    </button>

    <span class="font-semibold text-heading dark:text-heading-dark">
      Page {{ currentPage }} of {{ totalPages || 1 }} ({{ totalCount }} items)
    </span>

    <button
      :disabled="currentPage >= totalPages"
      @click="$emit('change-page', currentPage + 1)"
      class="px-4 py-2 bg-logo-blue text-white rounded disabled:opacity-50 hover:bg-logo-blue-dark transition"
    >
      Next
    </button>

    <div class="flex items-center gap-1">
      <label for="goToPage">Go to page:</label>
      <input
        id="goToPage"
        type="number"
        min="1"
        :max="totalPages"
        v-model.number="localPage"
        @keyup.enter="goToPage"
        class="w-16 px-2 py-1 border rounded"
      />
      <button @click="goToPage" class="px-2 py-1 bg-logo-blue text-white rounded">Go</button>
    </div>
  </div>
</template>

<script setup>
import { ref, watch } from 'vue'

const props = defineProps({
  currentPage: { type: Number, required: true },
  totalPages: { type: Number, required: true },
  totalCount: { type: Number, required: true },
})

const emit = defineEmits(['change-page'])

const localPage = ref(props.currentPage)

watch(
  () => props.currentPage,
  (newVal) => {
    localPage.value = newVal
  },
)

const goToPage = () => {
  const target = Math.min(Math.max(1, localPage.value), props.totalPages)
  if (target !== props.currentPage) {
    emit('change-page', target)
  }
}
</script>
