#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""Convert MP3 audio to LRC lyrics file using Whisper ASR model."""

import whisper
import os


def format_timestamp(seconds: float) -> str:
    """Convert seconds to LRC timestamp [mm:ss.xx]."""
    m = int(seconds // 60)
    s = int(seconds % 60)
    cs = int((seconds - int(seconds)) * 100)
    return f"[{m:02d}:{s:02d}.{cs:02d}]"


def generate_lrc(transcription, output_file):
    """Write LRC file from Whisper transcription segments."""
    with open(output_file, "w", encoding="utf-8") as f:
        for segment in transcription["segments"]:
            timestamp = format_timestamp(segment["start"])
            text = segment["text"].strip()
            f.write(f"{timestamp}{text}\n")
    print(f"LRC file saved to: {output_file}")


def main(audio_path):
    if not os.path.isfile(audio_path):
        print(f"File not found: {audio_path}")
        return

    print("Loading Whisper model...")
    model = whisper.load_model("large")  # tiny, base, small, medium, large

    print(f"Transcribing {audio_path} ...")
    result = model.transcribe(audio_path, fp16=False)

    lrc_file = os.path.splitext(audio_path)[0] + ".lrc"
    generate_lrc(result, lrc_file)

    print("Done!")


if __name__ == "__main__":
    import sys

    if len(sys.argv) < 2:
        print("Usage: python mp3_to_lrc.py song.mp3")
    else:
        main(sys.argv[1])
